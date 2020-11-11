using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused;
    public GameObject pauseMenu;
    public VoidEvent OnExitingLevel;

    InputMaster inputs;

    private void Awake()
    {
        inputs = new InputMaster();
        inputs.LevelMenu.Pause.performed += ctx => TogglePause();
        inputs.LevelMenu.Pause.Enable();
    }

    private void OnDisable() { inputs.LevelMenu.Pause.Disable(); }

    private void Start() { Resume(); }

    void TogglePause()
    {
        if (gamePaused)
            Resume();
        else
        {
            Pause();
            ShowPauseMenu(true);
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        gamePaused = false;
        ShowPauseMenu(false);
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        gamePaused = true;
    }

    void ShowPauseMenu(bool showMenu)
    {
        pauseMenu.SetActive(showMenu);
    }

    public void ExitToTitle()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        OnExitingLevel.Invoke();
        SceneManager.LoadScene("MainMenu"); // Exiting scene
    }
}
