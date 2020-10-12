using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(FPSMovementManager))]
[RequireComponent(typeof(FPSAnimationManager))]
public class FPSGeneral : MonoBehaviour
{
    InputMaster inputs;
    PlayerMovement playerMover;
    FPSMovementManager playerMovementManager;
    FPSAnimationManager playerAnimationManager;

    float health = 100;
    public Slider healthBar;

    int gunType = -1;

    // Reloading
    bool reloading = false;
    float reloadLength;

    // Pausing
    public static bool paused;

    private void Awake()
    {
        inputs = new InputMaster();
        inputs.Game.Reload.performed += ctx => StartCoroutine(Reload());
        inputs.Game.Fire.performed += ctx => Fire();
        inputs.Game.Sprinting.started += ctx => playerMovementManager.SetSprinting(true);
        inputs.Game.Sprinting.canceled += ctx => playerMovementManager.SetSprinting(false);
        inputs.Game.Scope.started += ctx => playerMovementManager.SetScoping(true);
        inputs.Game.Scope.canceled += ctx => playerMovementManager.SetScoping(false);
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


    void Fire()
    {
        if (paused || gunType == -1) return;

        if (!reloading && !playerMovementManager.GetSprinting())
            playerAnimationManager.PlayAnimation("Fire");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player Took Damage");
        health -= damage;
        healthBar.value = health / 100.0f;
        if (health <= 0)
            FindObjectOfType<PauseMenu>().ExitLevel();
    }

    void GetGun(int gunType)
    {

    }

    public void SetReloadLength(float newReloadLength) { reloadLength = newReloadLength; }
    public int GetGunType() { return gunType; }

    public void SetGunType(int setGunType) { gunType = setGunType; }

    // Waits until reload is finished before allowing other actions
    IEnumerator Reload()
    {
        if (paused || gunType == -1) yield break;

        if (!reloading)
        {
            reloading = true;
            playerAnimationManager.PlayAnimation("Reload");
            yield return new WaitForSecondsRealtime(reloadLength);
            reloading = false;
        }
    }
}
