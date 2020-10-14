/*
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class CallOfDutyGameplay : MonoBehaviour
{
    PlayerMovement playerMover;
    float startPlayerSpeed;
    const float SPRINT_SPEED_MULTIPLIER = 1.4f;
    const float SCOPE_SPEED_MULTIPLIER = 0.5f;
    const float RELOAD_SPEED_MULTIPLIER = 0.5f;
    public Camera cam;
    const float SPRINT_FOV = 75;
    const float STANDARD_FOV = 60;
    const float SCOPE_FOV = 40;
    const float FOV_LERP_FACTOR = 10;
    float reloadLength;
    const float RELOAD_CUTOFF = 1;

    public Animator gunHolderAnimator;
    public Animator gunAnimator;
    InputMaster inputs;

    // Player Variables
    float health = 1;

    // Animation Variables
    bool sprinting;
    bool scoping;
    bool reloading;

    // Pausing
    public static bool paused;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMovement>();
        startPlayerSpeed = playerMover.GetSpeed();

        inputs = new InputMaster();
        inputs.Game.Sprinting.started += ctx => sprinting = true;
        inputs.Game.Sprinting.canceled += ctx => sprinting = false;
        inputs.Game.Scope.started += ctx => scoping = true;
        inputs.Game.Scope.canceled += ctx => scoping = false;
        inputs.Game.Reload.performed += ctx => Reload();
        inputs.Game.Fire.performed += ctx => Fire();
        inputs.Game.Sprinting.Enable();
        inputs.Game.Scope.Enable();
        inputs.Game.Reload.Enable();
        inputs.Game.Fire.Enable();
    }

    private void Start() { RefreshReloadClipTime(); }

    private void OnDisable()
    {
        inputs.Game.Sprinting.Disable();
        inputs.Game.Scope.Disable();
        inputs.Game.Reload.Disable();
        inputs.Game.Fire.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (paused)
            return;

        // Updating continuous animations
        gunHolderAnimator.SetBool("Scoping", scoping && !sprinting && !reloading);
        gunHolderAnimator.SetBool("Running", sprinting && !reloading && playerMover.IsMoving() && !playerMover.IsMovingBackwards() && playerMover.IsGrounded());

        // Updating speed
        if (sprinting && !playerMover.IsMovingBackwards() && !reloading)
            playerMover.SetSpeed(startPlayerSpeed * SPRINT_SPEED_MULTIPLIER);
        else if (scoping && !reloading)
            playerMover.SetSpeed(startPlayerSpeed * SCOPE_SPEED_MULTIPLIER);
        else if (reloading)
            playerMover.SetSpeed(startPlayerSpeed * RELOAD_SPEED_MULTIPLIER);
        else
            playerMover.SetSpeed(startPlayerSpeed);

        // Updating FOV
        if (sprinting && !reloading && playerMover.IsMoving() && !playerMover.IsMovingBackwards() && playerMover.IsGrounded())
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, SPRINT_FOV, 1 / FOV_LERP_FACTOR);
        else if (scoping && !reloading)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, SCOPE_FOV, 1 / FOV_LERP_FACTOR);
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, STANDARD_FOV, 1 / FOV_LERP_FACTOR);
    }

    // Fires gun
    void Fire()
    {
        if (paused)
            return;
        if (!reloading && !sprinting)
            gunAnimator.SetTrigger("Fire");
    }

    // Called when reload button pressed. Calls reload delay coroutine
    void Reload()
    {
        if (paused)
            return;
        if (!reloading)
        {
            reloading = true;
            gunAnimator.SetTrigger("Reload");
            StartCoroutine(ReloadWaiting());
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        FindObjectOfType<Level01Controller>().RefreshHealthBar(health);
        if (health <= 0)
            FindObjectOfType<PauseMenu>().ExitLevel();
    }

    // Waits until reload is finished before allowing other actions
    IEnumerator ReloadWaiting()
    {
        yield return new WaitForSecondsRealtime(reloadLength);
        reloading = false;
    }

    // Updates length of reload animation for current gun
    public void RefreshReloadClipTime()
    {
        AnimationClip[] clips = gunAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
            if (clip.name == "Reload")
                reloadLength = clip.length - RELOAD_CUTOFF;
    }
}
*/