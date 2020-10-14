using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class FPSAnimationManager : MonoBehaviour
{
    PlayerMovement playerBaseMovement;

    public Animator gunHolderAnimator;
    Animator gunAnimator;

    // Animation Variables
    bool sprinting, scoping, reloading;

    // Pausing
    public static bool paused;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (paused) return;

        // Updating continuous animations
        gunHolderAnimator.SetBool("Scoping", gunAnimator != null && scoping && !sprinting && !reloading);
        gunHolderAnimator.SetBool("Running", sprinting && !reloading &&
            playerBaseMovement.GetMoving() && !playerBaseMovement.GetMovingBackwards() && playerBaseMovement.GetGrounded());
    }

    public void PlayAnimation(string animationToPlay)
    {
        if (gunAnimator != null)
            gunAnimator.SetTrigger(animationToPlay);
    }

    #region Mutators and Accessors

    public void SetSprinting(bool setSprinting) { sprinting = setSprinting; }
    public void SetScoping(bool setScoping) { scoping = setScoping; }
    public void SetReloading(bool setReloading) { reloading = setReloading; }
    public void SetGunAnimator(Animator gunAnimatorToSet) { gunAnimator = gunAnimatorToSet; }
    public void SetPlayerBaseMovement(PlayerMovement setPlayerBaseMovement) { playerBaseMovement = setPlayerBaseMovement; }

    #endregion
}
