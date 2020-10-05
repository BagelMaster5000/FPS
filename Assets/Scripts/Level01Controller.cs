using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01Controller : MonoBehaviour
{
    public TextMeshProUGUI curScoreTextView;

    int curScore;
    const int SCORE_INCREASE_AMT = 100;

    InputMaster inputs;

    // Setting up inputs
    private void Awake()
    {
        inputs = new InputMaster();
        inputs.LevelMenu.Exit.performed += ctx => ExitLevel();
        inputs.Game.IncreaseScore.performed += ctx => IncreaseScore(SCORE_INCREASE_AMT);
        inputs.Enable();
    }

    private void OnDisable() { inputs.Disable(); }

    void IncreaseScore(int scoreIncrease)
    {
        curScore += scoreIncrease;
        curScoreTextView.text = "Score: " + curScore;
    }

    private void ExitLevel()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (curScore > highScore) // Setting new highscore if necessary
            PlayerPrefs.SetInt("HighScore", curScore);
        Debug.Log("Current high score is " + PlayerPrefs.GetInt("HighScore"));
        SceneManager.LoadScene("MainMenu"); // Exiting scene
    }
}
