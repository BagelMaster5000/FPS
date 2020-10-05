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

    public Animator gunHolderAnimator;
    public Animator gunAnimator;
    InputMaster inputs;

    // Variables
    bool sprinting;
    bool scoping;
    bool reloading;

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
    void Update()
    {
        // Updating continuous animations
        gunHolderAnimator.SetBool("Scoping", scoping && !sprinting && !reloading);
        gunHolderAnimator.SetBool("Running", sprinting && !reloading && playerMover.IsMoving() && playerMover.IsGrounded());

        // Updating speed
        if (sprinting && !reloading)
            playerMover.SetSpeed(startPlayerSpeed * SPRINT_SPEED_MULTIPLIER);
        else if (scoping && !reloading)
            playerMover.SetSpeed(startPlayerSpeed * SCOPE_SPEED_MULTIPLIER);
        else if (reloading)
            playerMover.SetSpeed(startPlayerSpeed * RELOAD_SPEED_MULTIPLIER);
        else
            playerMover.SetSpeed(startPlayerSpeed);

        // Updating FOV
        if (sprinting && !reloading && playerMover.IsMoving())
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, SPRINT_FOV, 1 / FOV_LERP_FACTOR);
        else if (scoping && !reloading)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, SCOPE_FOV, 1 / FOV_LERP_FACTOR);
        else
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, STANDARD_FOV, 1 / FOV_LERP_FACTOR);
    }

    void Fire()
    {
        if (!reloading)
            gunAnimator.SetTrigger("Fire");
    }

    void Reload()
    {
        if (!reloading)
        {
            reloading = true;
            gunAnimator.SetTrigger("Reload");
            StartCoroutine(ReloadWaiting());
        }
    }

    IEnumerator ReloadWaiting()
    {
        yield return new WaitForSecondsRealtime(reloadLength);
        reloading = false;
    }

    public void RefreshReloadClipTime()
    {
        AnimationClip[] clips = gunAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
            if (clip.name == "Reload")
                reloadLength = clip.length;
        print("Reload length is " + reloadLength);
    }
}
