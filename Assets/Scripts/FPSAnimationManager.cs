using UnityEngine;

[RequireComponent(typeof(FPSGeneral))]
public class FPSAnimationManager : MonoBehaviour
{
    FPSGeneral fpsGeneral;

    public Animator gunHolderAnimator;
    Animator gunAnimator;

    private void Awake()
    {
        fpsGeneral = GetComponent<FPSGeneral>();
        fpsGeneral.OnGotNewGun += GotNewGun;
        fpsGeneral.OnGunFired += FireGun;
        fpsGeneral.OnReloadStarted += ReloadGun;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PauseMenu.gamePaused) return;

        // Updating continuous animations
        gunHolderAnimator.SetBool("Swapping", fpsGeneral.curGunState == FPSGeneral.GunState.SWAPPING);
        gunHolderAnimator.SetBool("Scoping", fpsGeneral.curGunState == FPSGeneral.GunState.SCOPING);
        gunHolderAnimator.SetBool("Running", fpsGeneral.curMoveState == FPSGeneral.MoveState.SPRINTING);
    }

    void GotNewGun() { SetGunAnimator(fpsGeneral.GetCurGunAnimator()); }
    void FireGun() { PlayAnimation("Fire"); }
    void ReloadGun() { PlayAnimation("Reload"); }

    public void PlayAnimation(string animationToPlay) { gunAnimator?.SetTrigger(animationToPlay); }

    #region Mutators and Accessors

    public void SetGunAnimator(Animator gunAnimatorToSet) { gunAnimator = gunAnimatorToSet; }

    #endregion
}
