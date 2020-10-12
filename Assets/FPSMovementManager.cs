using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class FPSMovementManager : MonoBehaviour
{
    PlayerMovement playerMover;
    public Camera cam;
    float startPlayerSpeed;
    const float SPRINT_SPEED_MULTIPLIER = 1.4f;
    const float SCOPE_SPEED_MULTIPLIER = 0.5f;
    const float RELOAD_SPEED_MULTIPLIER = 0.5f;
    const float SPRINT_FOV = 75;
    const float STANDARD_FOV = 60;
    const float SCOPE_FOV = 40;
    const float FOV_LERP_FACTOR = 10;
    float reloadLength;
    const float RELOAD_CUTOFF = 1;

    // Animation Variables
    bool sprinting;
    bool scoping;
    bool reloading;
    bool holdingGun;

    // Pausing
    public static bool paused;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMovement>();
        startPlayerSpeed = playerMover.GetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (paused)
            return;

        // Updating speed
        if (sprinting && !playerMover.IsMovingBackwards() && !reloading)
            playerMover.SetSpeed(startPlayerSpeed * SPRINT_SPEED_MULTIPLIER);
        else if (gunAnimator != null && scoping && !reloading)
            playerMover.SetSpeed(startPlayerSpeed * SCOPE_SPEED_MULTIPLIER);
        else if (reloading)
            playerMover.SetSpeed(startPlayerSpeed * RELOAD_SPEED_MULTIPLIER);
        else
            playerMover.SetSpeed(startPlayerSpeed);

        // Updating FOV
        if (sprinting && !reloading && playerMover.IsMoving() && !playerMover.IsMovingBackwards() && playerMover.IsGrounded())
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, SPRINT_FOV, 1 / FOV_LERP_FACTOR);
        else if (gunAnimator != null && scoping && !reloading)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, SCOPE_FOV, 1 / FOV_LERP_FACTOR);
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, STANDARD_FOV, 1 / FOV_LERP_FACTOR);
    }

    public void SetSprinting(bool setSprinting) { sprinting = setSprinting; }
    public void SetScoping(bool setScoping) { scoping = setScoping; }
    public void SetReloading(bool setReloading) { reloading = setReloading; }
    public void SetHoldingGun(bool setHoldingGun) { holdingGun = setHoldingGun; }

    public bool GetSprinting() { return sprinting; }
    public bool GetScoping() { return scoping; }
    public bool GetReloading() { return reloading; }
}
