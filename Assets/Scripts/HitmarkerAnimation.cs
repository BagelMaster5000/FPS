using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HitmarkerAnimation : MonoBehaviour
{
    const float START_SCALE = 1.85f;
    const float END_SCALE = 1.15f;
    float curScale = 1.6f;
    Image hitmarkerGraphic;

    bool animating;

    private void Start()
    {
        hitmarkerGraphic = GetComponent<Image>();
        hitmarkerGraphic.color = new Color(0, 0, 0, 0);
    }

    public void Appear(bool killingHit)
    {
        if (animating)
            StopAllCoroutines();

        if (killingHit)
            StartCoroutine(AppearAnimation(Color.red));
        else
            StartCoroutine(AppearAnimation(Color.white));
    }

    IEnumerator AppearAnimation(Color markerColor)
    {
        animating = true;
        curScale = START_SCALE;
        hitmarkerGraphic.color = markerColor;
        hitmarkerGraphic.CrossFadeAlpha(1, 0.1f, false);
        float timeLimit = Time.time + 0.5f;
        while (timeLimit > Time.time)
        {
            curScale = Mathf.Lerp(curScale, END_SCALE, 0.075f);
            transform.localScale = Vector3.one * curScale;
            yield return null;
        }
        hitmarkerGraphic.CrossFadeAlpha(0, 0.25f, false);
        yield return new WaitForSeconds(0.25f);
        animating = false;
    }
}
