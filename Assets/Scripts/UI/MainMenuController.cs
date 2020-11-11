using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI highScoreTextView;
    public MusicTrack startingSong;

    private void Start()
    {
        highScoreTextView.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");

        if (startingSong != null)
            AudioManager.PlaySongHelper(startingSong);
    }

    public void ResetSaveData()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        highScoreTextView.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
