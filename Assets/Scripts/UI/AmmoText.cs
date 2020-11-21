using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AmmoText : MonoBehaviour
{
    TextMeshProUGUI textAmmo;

    private void Awake()
    {
        textAmmo = GetComponent<TextMeshProUGUI>();
        textAmmo.text = "";
    }

    public void SetText(int ammoAmt)
    {
        if (ammoAmt < 0)
            textAmmo.text = "";
        else if (ammoAmt == 0)
            textAmmo.text = "OUT";
        else
            textAmmo.text = ammoAmt.ToString();

    }
}
