using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI highScoreTextView;
    public AudioClip startingSong;

    private void Start()
    {
        highScoreTextView.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");

        if (startingSong != null)
            AudioManager.PlaySongHelper(startingSong, 0.25f);
    }
}
