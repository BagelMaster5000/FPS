using UnityEngine;

public class GunCatchup : MonoBehaviour
{
    [SerializeField] private POVCameraDetachedLook looker;

    [SerializeField] private Transform positionTarget;
    [SerializeField] private Transform rotationLeftRightTarget;
    [SerializeField] private Transform rotationUpDownTarget;
    float curDampValue;
    float velocityUpDown = 0;
    float velocityLeftRight = 0;

    Quaternion curCatchupLoc;

    private void Update()
    {
        if (PauseMenu.gamePaused) return;

        curDampValue = 0.06f; // Mathf.Pow(0.06f, 1 / Time.unscaledDeltaTime / 60);
    }

    private void LateUpdate()
    {
        if (PauseMenu.gamePaused) return;

        transform.position = positionTarget.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, curCatchupLoc, 0.2f);
    }

    private void FixedUpdate()
    {
        curCatchupLoc = Quaternion.Euler(
            Vector3.right * Mathf.SmoothDampAngle(
                rotationUpDownTarget.eulerAngles.x,
                transform.rotation.eulerAngles.x,
                ref velocityUpDown,
                curDampValue) +
            Vector3.up * Mathf.SmoothDampAngle(
                rotationLeftRightTarget.eulerAngles.y,
                transform.rotation.eulerAngles.y,
                ref velocityLeftRight,
                curDampValue));
    }
}
