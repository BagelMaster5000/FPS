using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class FPSGeneral : MonoBehaviour
{
    // States
    public enum GunState { IDLE, RELOADING, SCOPING, SWAPPING};
    [HideInInspector] public GunState curGunState;
    public enum MoveState { IDLE, MOVING, SPRINTING, JUMPING };
    [HideInInspector] public MoveState curMoveState;

    // General
    [Header("General")]
    [SerializeField] Camera playerCam;
    InputMaster inputs;
    PlayerMovement playerMovement;

    // Movement
    float baseSpeed;
    bool sprintButtonDown;
    [SerializeField] float speedMultiplierSprint = 1.4f;
    [SerializeField] float speedMultiplierScope = 0.5f;
    [SerializeField] float speedMultiplierReload = 0.5f;

    // FOV
    [SerializeField] float fovSprint = 75;
    [SerializeField] float fovStandard = 60;
    [SerializeField] float fovLerpFactor = 10;

    // Health
    float health = 100;
    [SerializeField] float healthRegenDelay = 4;
    float healthRegenDelayer;
    [SerializeField] float healthRegenPerSecond = 15;
    public FloatEvent OnDamageTaken; // float is current health 0-100
    public FloatEvent OnHealthRegenerated; // float is current health 0-100
    public VoidEvent OnDead;

    // Guns
    [Header("Guns: Pistol, Shotgun")]
    [SerializeField] Gun[] allGuns;
    public BoolEvent OnGunHitTarget; // True when killing blow
    public StringEvent OnCurrentAmmoChanged; // string amount of ammo in magazine
    public StringEvent OnTotalAmmoChanged; // string amount of total ammo
    public event Action OnGunFired;
    public event Action OnGotNewGun;
    public event Action OnReloadStarted;
    int curGunSlot;
    public enum GunType { NONE = -1, PISTOL = 0, SHOTGUN = 1 };
    public struct HeldGunSlot
    {
        public GunType gunType;
        public int ammoInMag;
        public int ammoTotal;
    }
    [Range(1, 6)]
    [SerializeField] int numSlots = 2;
    HeldGunSlot[] heldGunSlots;
    Coroutine changingSlot;
    bool swapping;
    const float SWAP_MOMENT = 0.4f;
    const float SWAP_TOTAL_DURATION = 0.75f;
    public VoidEvent OnSwappingStarted;
    public VoidEvent OnSwappingEnded;

    #region Setup
    private void Awake()
    {
        inputs = new InputMaster();
        inputs.Game.Reload.performed += ctx => StartCoroutine(Reload());
        inputs.Game.Fire.started += ctx => Fire();
        inputs.Game.Sprinting.started += ctx => SprintStart();
        inputs.Game.Scope.started += ctx => StartScope();
        inputs.Game.Scope.canceled += ctx => EndScope();
        inputs.Game.SwapGun.started += ctx => ChangeActiveSlot();
        inputs.Game.SwapGun.Enable();
        inputs.Game.Reload.Enable();
        inputs.Game.Fire.Enable();
        inputs.Game.Sprinting.Enable();
        inputs.Game.Scope.Enable();

        playerMovement = GetComponent<PlayerMovement>();
        baseSpeed = playerMovement.GetSpeed();
        playerMovement.OnStartedMoving += MoveStart;
        playerMovement.OnStoppedMoving += SprintEnd;
        playerMovement.OnJumped += JumpStart;
        playerMovement.OnLanded += JumpEnd;
    }

    private void OnDisable()
    {
        inputs.Game.SwapGun.Disable();
        inputs.Game.Reload.Disable();
        inputs.Game.Fire.Disable();
        inputs.Game.Sprinting.Disable();
        inputs.Game.Scope.Disable();
    }

    private void Start()
    {
        heldGunSlots = new HeldGunSlot[numSlots];
        for (int n = 0; n < heldGunSlots.Length; n++)
        {
            heldGunSlots[n].gunType = GunType.NONE;
            heldGunSlots[n].ammoInMag = -1;
            heldGunSlots[n].ammoTotal = -1;
        }
        curGunSlot = 0;
        RefreshGunVisibility();

        StartCoroutine(HealthRegeneration());
        StartCoroutine(RefreshSpeedAndFOV());
        //ObtainGun(0);
    }
    #endregion

    #region Movement Extra
    void SprintStart()
    {
        if (playerMovement.GetMoving() && !playerMovement.GetMovingBackwards() && curMoveState == MoveState.MOVING &&
            curGunState != GunState.RELOADING && !swapping)
        {
            sprintButtonDown = true;

            curMoveState = MoveState.SPRINTING;
            curGunState = GunState.IDLE;
        }
    }
    void SprintEnd() { curMoveState = MoveState.IDLE; }

    void MoveStart() { curMoveState = (curMoveState == MoveState.IDLE) ? MoveState.MOVING : curMoveState; }
    void MoveEnd() { curMoveState = (curMoveState != MoveState.JUMPING) ? MoveState.IDLE : curMoveState; }

    void JumpStart() { curMoveState = MoveState.JUMPING; }
    void JumpEnd() { curMoveState = (curMoveState == MoveState.MOVING) ? MoveState.IDLE : MoveState.MOVING; }

    IEnumerator RefreshSpeedAndFOV()
    {
        while (true)
        {
            if (curMoveState == MoveState.IDLE || curGunState == GunState.SCOPING)
                sprintButtonDown = false;
            if (curMoveState == MoveState.MOVING && !playerMovement.GetMovingBackwards() &&
                sprintButtonDown && curGunState != GunState.RELOADING && !swapping)
                curMoveState = MoveState.SPRINTING;

            if (curMoveState == MoveState.SPRINTING)
            {
                playerMovement.SetSpeed(baseSpeed * speedMultiplierSprint);
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, fovSprint, 1 / fovLerpFactor);
            }
            else if (curGunState == GunState.SCOPING && heldGunSlots[curGunSlot].gunType != GunType.NONE)
            {
                playerMovement.SetSpeed(baseSpeed * speedMultiplierScope);
                playerCam.fieldOfView =
                    Mathf.Lerp(
                        playerCam.fieldOfView,
                        fovStandard - allGuns[(int)heldGunSlots[curGunSlot].gunType].gunProperties.scopeAmount,
                        1 / fovLerpFactor);
            }
            else if (curGunState == GunState.RELOADING && heldGunSlots[curGunSlot].gunType != GunType.NONE)
            {
                playerMovement.SetSpeed(baseSpeed * speedMultiplierReload);
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, fovStandard, 1 / fovLerpFactor);
            }
            else
            {
                playerMovement.SetSpeed(baseSpeed);
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, fovStandard, 1 / fovLerpFactor);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion

    #region Guns
    public void AddGun(GunType gunToAdd)
    {
        if (changingSlot != null)
            StopCoroutine(changingSlot);
        //SetCurGunType(gunType);
        if (heldGunSlots[curGunSlot].gunType == GunType.NONE)
        {
            StartCoroutine(AddGunAnimator(gunToAdd, curGunSlot));
            RefreshGunVisibility();
        }
    }

    IEnumerator AddGunAnimator(GunType gunToSwapIn, int slot = -1)
    {
        if (slot == -1)
        {
            slot = curGunSlot + 1;
            if (slot >= numSlots)
                slot = 0;
        }

        curGunState = GunState.SWAPPING;
        curMoveState = (curMoveState == MoveState.SPRINTING) ? MoveState.MOVING : curMoveState;
        swapping = true;
        yield return new WaitForSeconds(SWAP_MOMENT);
        heldGunSlots[slot].gunType = gunToSwapIn;
        heldGunSlots[slot].ammoInMag = allGuns[(int)gunToSwapIn].gunProperties.ammoMagazineSize;
        heldGunSlots[slot].ammoTotal = allGuns[(int)gunToSwapIn].gunProperties.ammoStarting;
        OnCurrentAmmoChanged.Invoke(heldGunSlots[slot].ammoInMag.ToString());
        OnTotalAmmoChanged.Invoke(heldGunSlots[slot].ammoTotal.ToString());
        RefreshGunVisibility();
        yield return new WaitForSeconds(SWAP_TOTAL_DURATION - SWAP_MOMENT);
        curGunState = GunState.IDLE;
        swapping = false;
    }

    void ChangeActiveSlot(int slot = -1)
    {
        if (curGunState == GunState.RELOADING || curGunState == GunState.SWAPPING) return;

        if (slot == -1)
        {
            slot = curGunSlot + 1;
            if (slot >= numSlots)
                slot = 0;
        }
        changingSlot = StartCoroutine(ChangeActiveSlotAnimator(slot));
    }

    IEnumerator ChangeActiveSlotAnimator(int slot)
    {
        if (slot < 0 || slot >= numSlots)
            Debug.LogError("Trying to change to invalid slot");
        curGunState = GunState.SWAPPING;
        curMoveState = (curMoveState == MoveState.SPRINTING) ? MoveState.MOVING : curMoveState;
        swapping = true;
        yield return new WaitForSeconds(SWAP_MOMENT);
        curGunSlot = slot;
        OnCurrentAmmoChanged.Invoke(heldGunSlots[slot].ammoInMag.ToString());
        OnTotalAmmoChanged.Invoke(heldGunSlots[slot].ammoTotal.ToString());
        RefreshGunVisibility();
        yield return new WaitForSeconds(SWAP_TOTAL_DURATION - SWAP_MOMENT);
        curGunState = GunState.IDLE;
        swapping = false;
    }

    void RefreshGunVisibility()
    {
        for (int n = 0; n < allGuns.Length; n++)
        {
            allGuns[n].gunAnimator.gameObject.SetActive((int)heldGunSlots[curGunSlot].gunType == n);
            OnGotNewGun.Invoke();
        }
    }

    void StartScope()
    {
        if (HoldingGun() && curGunState == GunState.IDLE)
        {
            curGunState = GunState.SCOPING;
            curMoveState = (curMoveState == MoveState.SPRINTING && curMoveState != MoveState.JUMPING) ? MoveState.MOVING : curMoveState;
        }
    }

    void EndScope() { curGunState = (curGunState == GunState.SCOPING) ?
            GunState.IDLE : curGunState; }

    void Fire()
    {
        if (PauseMenu.gamePaused || !HoldingGun() || heldGunSlots[curGunSlot].ammoInMag <= 0) return;

        if (curMoveState == MoveState.SPRINTING)
        {
            sprintButtonDown = false;
            curMoveState = MoveState.MOVING;
            return;
        }

        if (curGunState != GunState.RELOADING && !swapping && curMoveState != MoveState.SPRINTING)
        {
            OnGunFired.Invoke();
            heldGunSlots[curGunSlot].ammoInMag--;
            OnCurrentAmmoChanged.Invoke(heldGunSlots[curGunSlot].ammoInMag.ToString());

            Vector3 rayDirection = playerCam.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, rayDirection, out hit, 999))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Enemy is knocked-back when hit
                    hit.collider.attachedRigidbody.AddForce(rayDirection * 8, ForceMode.Impulse);
                    // Enemy takes damage and hitmarker appears. Hitmarker displays either white or red depending on if enemy was killed.
                    OnGunHitTarget.Invoke(hit.collider.GetComponent<EnemyGeneral>().TakeDamage(allGuns[(int)heldGunSlots[curGunSlot].gunType].gunProperties.damage));
                }
            }
        }
    }

    // Waits until reload is finished before allowing other actions
    IEnumerator Reload()
    {
        if (PauseMenu.gamePaused || !HoldingGun()) yield break;

        if (curGunState != GunState.RELOADING && !swapping)
        {
            curGunState = GunState.RELOADING;
            curMoveState = (curMoveState == MoveState.SPRINTING) ? MoveState.MOVING : curMoveState;

            // Starting reload animation
            OnReloadStarted.Invoke();
            yield return new WaitForSecondsRealtime(allGuns[(int)heldGunSlots[curGunSlot].gunType].gunProperties.reloadLength);

            // Reload executes
            int ammoAdding =
                Mathf.Clamp(
                    allGuns[(int)heldGunSlots[curGunSlot].gunType].gunProperties.ammoMagazineSize - heldGunSlots[curGunSlot].ammoInMag,
                    0,
                    heldGunSlots[curGunSlot].ammoTotal);
            heldGunSlots[curGunSlot].ammoInMag += ammoAdding;
            heldGunSlots[curGunSlot].ammoTotal -= ammoAdding;
            OnCurrentAmmoChanged.Invoke(heldGunSlots[curGunSlot].ammoInMag.ToString());
            OnTotalAmmoChanged.Invoke(heldGunSlots[curGunSlot].ammoTotal.ToString());
            curGunState = GunState.IDLE;
        }
    }

    bool HoldingGun() { return heldGunSlots[curGunSlot].gunType != GunType.NONE; }
    #endregion

    #region Health
    public void TakeDamage(float damageAmt)
    {
        health = Mathf.Clamp(health - damageAmt, 0, 100);
        OnDamageTaken.Invoke(health);
        healthRegenDelayer = Time.time + healthRegenDelay;
        if (health <= 0)
            OnDead.Invoke();
    }

    // Regenerates health if low after a delay
    IEnumerator HealthRegeneration()
    {
        while (true)
        {
            if (Time.time > healthRegenDelayer)
            {
                health = Mathf.Clamp(health + healthRegenPerSecond / 50.0f, 0, 100);
                OnHealthRegenerated.Invoke(health);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion

    #region Mutators and Accessors

    //public int GetCurGunType() { return curGunSlot; }
    public Animator GetCurGunAnimator()
    {
        if (heldGunSlots[curGunSlot].gunType != GunType.NONE)
            return allGuns[(int)heldGunSlots[curGunSlot].gunType].gunAnimator;
        else
            return null;
    }
    #endregion
}
