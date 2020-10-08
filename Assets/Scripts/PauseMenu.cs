using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused;
    public GameObject pauseMenu;

    InputMaster inputs;

    private void Awake()
    {
        inputs = new InputMaster();
        inputs.LevelMenu.Pause.performed += ctx => TogglePause();
        inputs.LevelMenu.Pause.Enable();
    }

    private void OnDisable() { inputs.LevelMenu.Pause.Disable(); }

    private void Start()
    {
        Resume();
    }

    void TogglePause()
    {
        if (gamePaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;

        SetObjectPause();
    }

    void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;

        SetObjectPause();
    }

    // Toggles pauses of objects
    void SetObjectPause()
    {
        CallOfDutyGameplay.paused = gamePaused;
        Look.paused = gamePaused;
    }

    public void ExitLevel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (Level01Controller.curScore > highScore) // Setting new highscore if necessary
            PlayerPrefs.SetInt("HighScore", Level01Controller.curScore);
        Debug.Log("Current high score is " + PlayerPrefs.GetInt("HighScore"));
        SceneManager.LoadScene("MainMenu"); // Exiting scene
    }
}
