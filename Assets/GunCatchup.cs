using UnityEngine;

public class GunCatchup : MonoBehaviour
{
    [SerializeField] private Look looker;

    [SerializeField] private Transform positionTarget;
    [SerializeField] private Transform rotationLeftRightTarget;
    [SerializeField] private Transform rotationUpDownTarget;
    float curDampValue;
    float dummy1 = 0;
    float dummy2 = 0;

    private void Update() { curDampValue = 1 - Mathf.Pow(0.99f, Time.deltaTime * 60); }

    private void LateUpdate()
    {
        transform.position = positionTarget.position;
        transform.rotation = Quaternion.Euler(
            Vector3.right * Mathf.SmoothDampAngle(
                rotationUpDownTarget.eulerAngles.x,
                transform.rotation.eulerAngles.x,
                ref dummy1,
                curDampValue) +
            Vector3.up * Mathf.SmoothDampAngle(
                rotationLeftRightTarget.eulerAngles.y,
                transform.rotation.eulerAngles.y,
                ref dummy2,
                curDampValue));
    }
}
