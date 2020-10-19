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
            ShowMenu(true);
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        gamePaused = false;
        ShowMenu(false);

        SetObjectPause();
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        gamePaused = true;

        SetObjectPause();
    }

    /* Shows or hides pause menu depending on parameter
     * @param showMenu true if menu is to be shown
     */
    void ShowMenu(bool showMenu)
    {
        pauseMenu.SetActive(showMenu);
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
