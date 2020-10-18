using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    Slider healthBar;

    private void Start() { healthBar = GetComponent<Slider>(); }

    public void SetHealthBarValue(float setHealth) { healthBar.value = setHealth / 100.0f; }
}
