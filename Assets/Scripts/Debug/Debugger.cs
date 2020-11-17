using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    TextMeshProUGUI debugText;
    public FPSGeneral player;

    // Start is called before the first frame update
    void Awake()
    {
        debugText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text =
            "Player State: " + player.curMoveState + "\n" +
            "Gun State: " + player.curGunState;
    }
}
