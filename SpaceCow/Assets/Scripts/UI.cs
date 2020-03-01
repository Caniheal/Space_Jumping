using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void OnRetry()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
