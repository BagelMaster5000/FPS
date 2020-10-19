using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(FPSMovementManager))]
[RequireComponent(typeof(FPSAnimationManager))]
public class FPSGeneral : MonoBehaviour
{
    // General
    [Header("General")]
    public Camera playerCam;
    InputMaster inputs;
    FPSMovementManager playerMovementManager;
    FPSAnimationManager playerAnimationManager;

    // Events
    [Header("Events")]
    public BoolEvent OnGunHitTarget; // True when killing blow
    public FloatEvent OnDamageTaken; // float is current health 0-100
    public FloatEvent OnHealthRegenerated; // float is current health 0-100
    public StringEvent OnCurrentAmmoChanged; // int amount of ammo in magazine
    public StringEvent OnTotalAmmoChanged; // int amount of total ammo
    public VoidEvent OnDead;

    // Health
    float health = 100;
    const float HEALTH_REGEN_DELAY = 4;
    float healthRegenDelayer;
    const float HEALTH_REGEN_PER_SECOND = 15;

    // Guns
    [Header("Guns")]
    public Gun[] allGuns;
    int curGunType = -1;
    int ammoCurrent = 0;
    int ammoTotal = 0;

    // Reloading
    bool reloading = false;

    // Pausing
    public static bool paused;

    private void Awake()
    {
        playerAnimationManager = GetComponent<FPSAnimationManager>();
        playerMovementManager = GetComponent<FPSMovementManager>();
        playerMovementManager.SetPlayerBaseMovement(GetComponent<PlayerMovement>());
        playerAnimationManager.SetPlayerBaseMovement(GetComponent<PlayerMovement>());

        inputs = new InputMaster();
        inputs.Game.Reload.performed += ctx => StartCoroutine(Reload());
        inputs.Game.Fire.performed += ctx => Fire();
        inputs.Game.Sprinting.started += ctx => playerMovementManager.SetSprinting(true);
        inputs.Game.Sprinting.started += ctx => playerAnimationManager.SetSprinting(true);
        inputs.Game.Sprinting.canceled += ctx => playerMovementManager.SetSprinting(false);
        inputs.Game.Sprinting.canceled += ctx => playerAnimationManager.SetSprinting(false);
        inputs.Game.Scope.started += ctx => playerMovementManager.SetScoping(true);
        inputs.Game.Scope.started += ctx => playerAnimationManager.SetScoping(true);
        inputs.Game.Scope.canceled += ctx => playerMovementManager.SetScoping(false);
        inputs.Game.Scope.canceled += ctx => playerAnimationManager.SetScoping(false);
        inputs.Game.Reload.Enable();
        inputs.Game.Fire.Enable();
        inputs.Game.Sprinting.Enable();
        inputs.Game.Scope.Enable();
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
        ObtainGun(0);
    }

    void Fire()
    {
        if (paused || curGunType == -1 || ammoCurrent <= 0) return;

        if (!reloading && !playerMovementManager.GetSprinting())
        {
            playerAnimationManager.PlayAnimation("Fire");
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

    public void TakeDamage(float damageAmt)
    {
        health = Mathf.Clamp(health - damageAmt, 0, 100);
        OnDamageTaken.Invoke(health);
        healthRegenDelayer = Time.time + HEALTH_REGEN_DELAY;
        if (health <= 0)
        {
            OnDead.Invoke();
        }
    }

    void ObtainGun(int gunType)
    {
        SetCurGunType(gunType);
        ammoCurrent = allGuns[gunType].gunType.ammoMagazineSize;
        ammoTotal = allGuns[gunType].gunType.ammoStarting;
        OnCurrentAmmoChanged.Invoke(ammoCurrent.ToString());
        OnTotalAmmoChanged.Invoke(ammoTotal.ToString());
        playerMovementManager.SetCurrentScopeAmt(allGuns[curGunType].gunType.scopeAmount);
    }

    #region Mutators and Accessors

    public void SetCurGunType(int setGunType)
    {
        if (setGunType < -1 || setGunType >= allGuns.Length) return;

        playerMovementManager.SetHoldingGun(setGunType > -1);
        curGunType = setGunType;
        playerAnimationManager.SetGunAnimator(allGuns[curGunType].gunAnimator);
        for (int n = 0; n < allGuns.Length; n++)
            allGuns[n].gunAnimator.gameObject.SetActive(n == curGunType);
    }

    public int GetCurGunType() { return curGunType; }

    #endregion

    // Regenerates health if low after a delay
    IEnumerator HealthRegeneration()
    {
        while (true)
        {
            if (Time.time > healthRegenDelayer)
            {
                health = Mathf.Clamp(health + HEALTH_REGEN_PER_SECOND / 50.0f, 0, 100);
                OnHealthRegenerated.Invoke(health);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    // Waits until reload is finished before allowing other actions
    IEnumerator Reload()
    {
        if (paused || curGunType == -1) yield break;

        if (!reloading)
        {
            // Starting reload animation
            playerMovementManager.SetReloading(true);
            playerAnimationManager.SetReloading(true);
            playerAnimationManager.PlayAnimation("Reload");
            yield return new WaitForSecondsRealtime(allGuns[curGunType].gunType.reloadLength);

            // Reload executes
            playerMovementManager.SetReloading(false);
            playerAnimationManager.SetReloading(false);
            int ammoAdding = Mathf.Clamp((allGuns[curGunType].gunType.ammoMagazineSize - ammoCurrent), 0, ammoTotal);
            ammoCurrent += ammoAdding;
            ammoTotal -= ammoAdding;
            OnCurrentAmmoChanged.Invoke(ammoCurrent.ToString());
            OnTotalAmmoChanged.Invoke(ammoTotal.ToString());
        }
    }
}
