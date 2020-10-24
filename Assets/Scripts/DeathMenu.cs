using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{ 
    public GameObject deathMenu;

    public VoidEvent OnRestartingLevel;
    public VoidEvent OnExitingLevel;

    public void ShowMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        deathMenu.SetActive(true);
    }

    public void Restart()
    {
        OnRestartingLevel.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitLevel()
    {
        OnExitingLevel.Invoke();
        SceneManager.LoadScene("MainMenu"); // Exiting scene
    }
}
