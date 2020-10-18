using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused;
    public GameObject pauseMenu;
    public UnityEvent OnExitingLevel;

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
        Look.paused = gamePaused;
        FPSGeneral.paused = gamePaused;
    }

    public void ExitLevel()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        OnExitingLevel.Invoke();
        SceneManager.LoadScene("MainMenu"); // Exiting scene
    }
}
