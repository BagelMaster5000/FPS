using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    float shakeAmt;
    const float SHAKE_ENDING_SMOOTHNESS = 5;
    const float NOT_SHAKING_THRESHOLD = 0.01f;

    private void Update()
    {
        if (PauseMenu.gamePaused) return;

        if (shakeAmt > 0)
            transform.localPosition = Vector2.up * Random.Range(0, shakeAmt) + Vector2.right * Random.Range(0, shakeAmt);
    }

    private void FixedUpdate()
    {
        if (shakeAmt > NOT_SHAKING_THRESHOLD)
        {
            shakeAmt = Mathf.Lerp(shakeAmt, 0, 1 / SHAKE_ENDING_SMOOTHNESS);
            if (shakeAmt <= NOT_SHAKING_THRESHOLD)
                shakeAmt = 0;
        }
    }

    public void ShakeCamera(float setShakeAmt) { shakeAmt = setShakeAmt; }
}
