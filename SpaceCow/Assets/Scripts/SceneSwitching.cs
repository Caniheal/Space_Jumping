using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    private void Start()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void SettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits Menu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }
}
