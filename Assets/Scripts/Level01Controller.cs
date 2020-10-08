using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    public TextMeshProUGUI curScoreTextView;

    public static int curScore;
    public Slider healthBar;
    const int SCORE_INCREASE_AMT = 100;

    InputMaster inputs;

    // Setting up inputs
    private void Awake()
    {
        healthBar.value = 1;

        inputs = new InputMaster();
        inputs.Game.IncreaseScore.performed += ctx => IncreaseScore(SCORE_INCREASE_AMT);
        inputs.Game.IncreaseScore.Enable();
    }

    private void OnDisable() { inputs.Game.IncreaseScore.Disable(); }

    private void Start()
    {
        Camera.main.enabled = false;
    }

    void IncreaseScore(int scoreIncrease)
    {
        curScore += scoreIncrease;
        curScoreTextView.text = "Score: " + curScore;
    }

    public void RefreshHealthBar(float health)
    {
        healthBar.value = health;
    }
}
