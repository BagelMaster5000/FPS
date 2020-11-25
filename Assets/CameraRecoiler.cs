using UnityEngine;

public class CameraRecoiler : MonoBehaviour
{
    float recoilCurDistance = 0;

    float recoilDiminishDistanceThreshold = 0.1f;
    int recoilDiminishIncrementer = 1;

    float recoilDownAcceleration = 0.1f;
    float recoilCurVelocity;

    private void Update()
    {
        if (PauseMenu.gamePaused) return;

        //if (recoilCurDistance > 0)
        //    print(recoilCurDistance);

        transform.localRotation =
            Quaternion.AngleAxis(
                Mathf.LerpAngle(
                    transform.localRotation.eulerAngles.x,
                    -recoilCurDistance, 0.5f),
                    Vector3.right);
    }

    private void FixedUpdate()
    {
        if (recoilCurDistance > 0 || recoilCurVelocity > 0)
        {
            recoilCurDistance += recoilCurVelocity;
            recoilCurVelocity -= recoilDownAcceleration;

            if (recoilCurDistance < 0)
                recoilCurDistance = 0;
        }
    }

    public void Recoil(float setRecoilDistanceUp)
    {
        float recoilDistanceGoal = recoilCurDistance + setRecoilDistanceUp / recoilDiminishIncrementer;
        if (recoilCurDistance >= recoilDiminishDistanceThreshold)
            recoilDiminishIncrementer++;
        else
            recoilDiminishIncrementer = 1;

        recoilCurVelocity = Mathf.Sqrt(2 * recoilDownAcceleration * setRecoilDistanceUp / recoilDiminishIncrementer);
        recoilCurVelocity -= recoilDownAcceleration / 2; // Adjustment so that physics equation is correct
    }
}
