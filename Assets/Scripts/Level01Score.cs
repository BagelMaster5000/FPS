using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Level01Score : MonoBehaviour
{
    TextMeshProUGUI textScoreDisplay;
    public static int playerScore = 0;

    const int HIT_VALUE = 100;
    const int KILL_VALUE = 1000;

    void Start()
    {
        playerScore = 0;
        textScoreDisplay = GetComponent<TextMeshProUGUI>();
        RefreshScoreDisplay();
    }

    // Gives player points when enemy is hit and/or killed
    public void GotHit(bool gotKill)
    {
        playerScore += HIT_VALUE;
        if (gotKill)
            playerScore += KILL_VALUE;
        RefreshScoreDisplay();
    }

    void RefreshScoreDisplay() { textScoreDisplay.text = "Score: " + playerScore; }

    public void RefreshHighScore()
    {
        if (Level01Score.playerScore > PlayerPrefs.GetInt("HighScore")) // Setting new highscore if necessary
            PlayerPrefs.SetInt("HighScore", Level01Score.playerScore);
    }
}
