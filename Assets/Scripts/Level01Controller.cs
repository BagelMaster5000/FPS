using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level01Controller : MonoBehaviour
{
    InputMaster inputs;

    #region Input Set-Up

    private void Awake()
    {
        inputs = new InputMaster();
        inputs.LevelMenu.Exit.performed += ctx => ExitLevel();
        inputs.LevelMenu.Enable();
    }

    private void OnDisable() { inputs.LevelMenu.Disable(); }

    #endregion

    private void ExitLevel() { SceneManager.LoadScene("MainMenu"); }
}
