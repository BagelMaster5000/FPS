using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PlayerScore : MonoBehaviour
{
    TextMeshProUGUI textScoreDisplay;
    int score = 0;

    const int HIT_VALUE = 100;
    const int KILL_VALUE = 1000;

    void Start()
    {
        score = 0;
        textScoreDisplay = GetComponent<TextMeshProUGUI>();
        RefreshScoreDisplay();
    }

    // Gives player points when enemy is hit and/or killed
    public void GotHit(bool gotKill)
    {
        score += HIT_VALUE;
        if (gotKill)
            score += KILL_VALUE;
        RefreshScoreDisplay();
    }

    public void SubtractFromScore(int subtractAmt)
    {
        score -= subtractAmt;
        RefreshScoreDisplay();
    }

    void RefreshScoreDisplay() { textScoreDisplay.text = "Score: " + score; }

    public int GetCurrentScore() { return score; }

    /*
    public void RefreshHighScore()
    {
        if (playerScore > PlayerPrefs.GetInt("HighScore")) // Setting new highscore if necessary
            PlayerPrefs.SetInt("HighScore", playerScore);
    }
    */
}
