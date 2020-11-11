using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthLowBorder : MonoBehaviour
{
    Image healthLowBorder;
    const float TIME_HOLD = 0.3f;
    const float TIME_FADE = 2f;
    float lastRecievedHealthValue = 100;

    const float MAX_HEALTH_CONTINUOUS_WARNING = 40;

    // Start is called before the first frame update
    void Start()
    {
        healthLowBorder = GetComponent<Image>();
        healthLowBorder.CrossFadeAlpha(0, 0, true);
        healthLowBorder.pixelsPerUnitMultiplier = 0;
    }

    public void HealthWarning(float health)
    {
        StopAllCoroutines();
        StartCoroutine(HealthWarningFadeOut(health < MAX_HEALTH_CONTINUOUS_WARNING));
        lastRecievedHealthValue = health;
    }

    public void HealthRegenerated(float health)
    {
        if (lastRecievedHealthValue >= MAX_HEALTH_CONTINUOUS_WARNING) return;

        if (health >= MAX_HEALTH_CONTINUOUS_WARNING)
            healthLowBorder.CrossFadeAlpha(0, TIME_FADE, false);
        lastRecievedHealthValue = health;
    }

    IEnumerator HealthWarningFadeOut(bool continuousHold)
    {
        // Holding
        healthLowBorder.CrossFadeAlpha(1, 0, true);
        healthLowBorder.pixelsPerUnitMultiplier = 0.2f;
        yield return new WaitForSeconds(TIME_HOLD);

        // Fading out
        if (!continuousHold)
            healthLowBorder.CrossFadeAlpha(0, TIME_FADE, false);
        float timeToFadeUntil = Time.time + TIME_FADE;
        while (timeToFadeUntil > Time.time)
        {
            healthLowBorder.pixelsPerUnitMultiplier = 0.5f + (healthLowBorder.pixelsPerUnitMultiplier - 0.5f) * 0.95f ;
            yield return new WaitForFixedUpdate();
        }
        healthLowBorder.pixelsPerUnitMultiplier = 0.5f;
    }
}
