using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class FPSGeneral : MonoBehaviour
{
    // States
    public enum GunState { NONE, RELOADING, SCOPING };
    [HideInInspector] public GunState playerGunState;
    public enum MoveState { STILL, MOVING, SPRINTING, JUMPING };
    [HideInInspector] public MoveState playerMoveState;

    // General
    [Header("General")]
    [SerializeField] Camera playerCam;
    InputMaster inputs;
    PlayerMovement playerMovement;

    // Events
    [Header("Events")]
    public BoolEvent OnGunHitTarget; // True when killing blow
    public FloatEvent OnDamageTaken; // float is current health 0-100
    public FloatEvent OnHealthRegenerated; // float is current health 0-100
    public StringEvent OnCurrentAmmoChanged; // string amount of ammo in magazine
    public StringEvent OnTotalAmmoChanged; // string amount of total ammo
    public VoidEvent OnDead;
    public event Action OnGunFired;
    public event Action OnGotNewGun;
    public event Action OnReloadStarted;

    // Movement
    float baseSpeed;
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

    // Guns
    [Header("Guns")]
    [SerializeField] Gun[] allGuns;
    int curGunType = -1;
    int ammoCurrent = 0;
    int ammoTotal = 0;

    // Pausing
    public static bool paused;

    private void Awake()
    {
        inputs = new InputMaster();
        inputs.Game.Reload.performed += ctx => StartCoroutine(Reload());
        inputs.Game.Fire.started += ctx => Fire();
        inputs.Game.Sprinting.started += ctx => SprintStart();
        inputs.Game.Scope.started += ctx => StartScope();
        inputs.Game.Scope.canceled += ctx => EndScope();
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
        inputs.Game.Reload.Disable();
        inputs.Game.Fire.Disable();
        inputs.Game.Sprinting.Disable();
        inputs.Game.Scope.Disable();
    }

    private void Start()
    {
        StartCoroutine(HealthRegeneration());
        StartCoroutine(RefreshSpeedAndFOV());
        ObtainGun(0);
    }

    #region Movement Extra
    void SprintStart()
    {
        if (playerMovement.GetMoving() && !playerMovement.GetMovingBackwards() && playerMoveState == MoveState.MOVING &&
            playerGunState != GunState.RELOADING)
        {
            playerMoveState = MoveState.SPRINTING;
            playerGunState = GunState.NONE;
        }
    }
    void SprintEnd() { playerMoveState = MoveState.STILL; }

    void MoveStart() { playerMoveState = (playerMoveState == MoveState.STILL) ? MoveState.MOVING : playerMoveState; }
    void MoveEnd() { playerMoveState = (playerMoveState != MoveState.JUMPING) ? MoveState.STILL : playerMoveState; }

    void JumpStart() { playerMoveState = MoveState.JUMPING; print("JUMP START"); }
    void JumpEnd() { playerMoveState = (playerMoveState == MoveState.MOVING) ? MoveState.STILL : MoveState.MOVING; print("JUMP END"); }

    IEnumerator RefreshSpeedAndFOV()
    {
        while (true)
        {
            if (playerMoveState == MoveState.SPRINTING)
            {
                playerMovement.SetSpeed(baseSpeed * speedMultiplierSprint);
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, fovSprint, 1 / fovLerpFactor);
            }
            else if (playerGunState == GunState.SCOPING)
            {
                playerMovement.SetSpeed(baseSpeed * speedMultiplierScope);
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, fovStandard - allGuns[curGunType].gunType.scopeAmount, 1 / fovLerpFactor);
            }
            else if (playerGunState == GunState.RELOADING)
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
    void ObtainGun(int gunType)
    {
        SetCurGunType(gunType);
        ammoCurrent = allGuns[gunType].gunType.ammoMagazineSize;
        ammoTotal = allGuns[gunType].gunType.ammoStarting;
        OnCurrentAmmoChanged.Invoke(ammoCurrent.ToString());
        OnTotalAmmoChanged.Invoke(ammoTotal.ToString());
    }

    void StartScope()
    {
        if (HoldingGun() && playerGunState == GunState.NONE)
        {
            playerGunState = GunState.SCOPING;
            playerMoveState = (playerMoveState == MoveState.SPRINTING && playerMoveState != MoveState.JUMPING) ? MoveState.MOVING : playerMoveState;
        }
    }

    void EndScope() { playerGunState = (playerGunState == GunState.SCOPING) ?
            GunState.NONE : playerGunState; }

    void Fire()
    {
        if (paused || !HoldingGun() || ammoCurrent <= 0) return;

        if (playerGunState != GunState.RELOADING && playerMoveState != MoveState.SPRINTING)
        {
            OnGunFired.Invoke();
            ammoCurrent--;
            OnCurrentAmmoChanged.Invoke(ammoCurrent.ToString());

            Vector3 rayDirection = playerCam.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(playerCam.transform.position, rayDirection, out hit, 999))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    // Enemy is knocked-back when hit
                    hit.collider.attachedRigidbody.AddForce(rayDirection * 8, ForceMode.Impulse);
                    // Enemy takes damage and hitmarker appears. Hitmarker displays either white or red depending on if enemy was killed.
                    OnGunHitTarget.Invoke(hit.collider.GetComponent<EnemyGeneral>().TakeDamage(allGuns[curGunType].gunType.damage));
                }
            }
        }
    }

    // Waits until reload is finished before allowing other actions
    IEnumerator Reload()
    {
        if (paused || !HoldingGun()) yield break;

        if (playerGunState != GunState.RELOADING)
        {
            playerGunState = GunState.RELOADING;
            playerMoveState = (playerMoveState == MoveState.SPRINTING) ? MoveState.MOVING : playerMoveState;

            // Starting reload animation
            OnReloadStarted.Invoke();
            yield return new WaitForSecondsRealtime(allGuns[curGunType].gunType.reloadLength);

            // Reload executes
            int ammoAdding = Mathf.Clamp((allGuns[curGunType].gunType.ammoMagazineSize - ammoCurrent), 0, ammoTotal);
            ammoCurrent += ammoAdding;
            ammoTotal -= ammoAdding;
            OnCurrentAmmoChanged.Invoke(ammoCurrent.ToString());
            OnTotalAmmoChanged.Invoke(ammoTotal.ToString());
            playerGunState = GunState.NONE;
        }
    }

    bool HoldingGun() { return curGunType != -1; }
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
    public void SetCurGunType(int setGunType)
    {
        if (setGunType < -1 || setGunType >= allGuns.Length) return;

        curGunType = setGunType;
        OnGotNewGun.Invoke();
        for (int n = 0; n < allGuns.Length; n++)
            allGuns[n].gunAnimator.gameObject.SetActive(n == curGunType);
    }

    public int GetCurGunType() { return curGunType; }
    public Animator GetCurGunAnimator() { return allGuns[curGunType].gunAnimator; }
    #endregion
}
