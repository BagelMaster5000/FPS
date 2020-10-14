﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(FPSMovementManager))]
[RequireComponent(typeof(FPSAnimationManager))]
public class FPSGeneral : MonoBehaviour
{
    InputMaster inputs;
    FPSMovementManager playerMovementManager;
    FPSAnimationManager playerAnimationManager;

    float health = 100;
    public Slider healthBar;

    public Gun[] allGuns;
    int curGunType = -1;

    // Reloading
    bool reloading = false;
    float reloadLength;

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
        ObtainGun(0);
    }

    void Fire()
    {
        if (paused || curGunType == -1) return;

        if (!reloading && !playerMovementManager.GetSprinting())
            playerAnimationManager.PlayAnimation("Fire");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health / 100.0f;
        if (health <= 0)
            FindObjectOfType<PauseMenu>().ExitLevel();
    }

    void ObtainGun(int gunType)
    {
        SetCurGunType(gunType);
    }

    #region Mutators and Accessors

    public void SetReloadLength(float newReloadLength) { reloadLength = newReloadLength; }
    public void SetCurGunType(int setGunType)
    {
        if (setGunType < -1 || setGunType >= allGuns.Length) return;

        curGunType = setGunType;
        playerAnimationManager.SetGunAnimator(allGuns[curGunType].gunAnimator);
        for (int n = 0; n < allGuns.Length; n++)
            allGuns[n].gunAnimator.gameObject.SetActive(n == curGunType);
    }

    public int GetCurGunType() { return curGunType; }

    #endregion

    // Waits until reload is finished before allowing other actions
    IEnumerator Reload()
    {
        if (paused || curGunType == -1) yield break;

        if (!reloading)
        {
            playerMovementManager.SetReloading(true);
            playerAnimationManager.SetReloading(true);
            playerAnimationManager.PlayAnimation("Reload");
            yield return new WaitForSecondsRealtime(reloadLength);
            playerMovementManager.SetReloading(false);
            playerAnimationManager.SetReloading(false);
        }
    }
}
