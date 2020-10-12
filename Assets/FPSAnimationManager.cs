using UnityEngine;

[RequireComponent(typeof(FPSMovementAdditions))]
[RequireComponent(typeof(PlayerMovement))]
public class FPSAnimationManager : MonoBehaviour
{
    PlayerMovement playerMover;
    FPSMovementAdditions fpsMovement;

    public Animator gunHolderAnimator;
    public Animator gunAnimator;

    // Animation Variables
    bool sprinting;
    bool scoping;
    bool reloading;

    // Pausing
    public static bool paused;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMovement>();
        fpsMovement = GetComponent<FPSMovementAdditions>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (paused)
            return;

        // Updating continuous animations
        sprinting = fpsMovement.IsSprinting();
        scoping = fpsMovement.IsScoping();
        reloading = fpsMovement.IsReloading();
        gunHolderAnimator.SetBool("Scoping", gunAnimator != null && scoping && !sprinting && !reloading);
        gunHolderAnimator.SetBool("Running", sprinting && !reloading && playerMover.IsMoving() &&!playerMover.IsMovingBackwards() && playerMover.IsGrounded());
    }

    public void PlayAnimation(string animationToPlay)
    {
        if (gunAnimator != null)
            gunAnimator.SetTrigger(animationToPlay);
    }

    /* Sets gunAnimator variable. Called when gun is changed.
     * @param gunAnimatorToSet variable to update gunAnimator with
     */
    public void SetGunAnimator(Animator gunAnimatorToSet) { gunAnimator = gunAnimatorToSet; }
}
