using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class FPSMovementManager : MonoBehaviour
{
    PlayerMovement playerBaseMovement;

    public Camera cam;
    float startPlayerSpeed;
    const float SPRINT_SPEED_MULTIPLIER = 1.4f;
    const float SCOPE_SPEED_MULTIPLIER = 0.5f;
    const float RELOAD_SPEED_MULTIPLIER = 0.5f;
    const float SPRINT_FOV = 75;
    const float STANDARD_FOV = 60;
    float currentScopeAmt = 20;
    const float FOV_LERP_FACTOR = 10;

    // Animation Variables
    bool sprinting, scoping, reloading, holdingGun;

    // Pausing
    public static bool paused;

    private void Awake()
    {
        playerBaseMovement = GetComponent<PlayerMovement>();
        startPlayerSpeed = playerBaseMovement.GetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (paused) return;

        // Updating speed
        if (sprinting && !playerBaseMovement.GetMovingBackwards() && !reloading)
            playerBaseMovement.SetSpeed(startPlayerSpeed * SPRINT_SPEED_MULTIPLIER);
        else if (holdingGun && scoping && !reloading)
            playerBaseMovement.SetSpeed(startPlayerSpeed * SCOPE_SPEED_MULTIPLIER);
        else if (reloading)
            playerBaseMovement.SetSpeed(startPlayerSpeed * RELOAD_SPEED_MULTIPLIER);
        else
            playerBaseMovement.SetSpeed(startPlayerSpeed);

        // Updating FOV
        if (sprinting && !reloading && playerBaseMovement.GetMoving() && !playerBaseMovement.GetMovingBackwards() && playerBaseMovement.GetGrounded())
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, SPRINT_FOV, 1 / FOV_LERP_FACTOR);
        else if (holdingGun && scoping && !reloading)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, STANDARD_FOV - currentScopeAmt, 1 / FOV_LERP_FACTOR);
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, STANDARD_FOV, 1 / FOV_LERP_FACTOR);
    }

    #region Mutators and Accessors

    public void SetSprinting(bool setSprinting) { sprinting = setSprinting; }
    public void SetScoping(bool setScoping) { scoping = setScoping; }
    public void SetReloading(bool setReloading) { reloading = setReloading; }
    public void SetHoldingGun(bool setHoldingGun) { holdingGun = setHoldingGun; }
    public void SetPlayerBaseMovement(PlayerMovement setPlayerBaseMovement) { playerBaseMovement = setPlayerBaseMovement; }
    public void SetCurrentScopeAmt(float setCurrentScopeAmt) { currentScopeAmt = setCurrentScopeAmt; }

    public bool GetSprinting() { return sprinting; }
    public bool GetScoping() { return scoping; }
    public bool GetReloading() { return reloading; }

    #endregion
}
