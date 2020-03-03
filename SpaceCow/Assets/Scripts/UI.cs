using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnRetry()
    {
        //audioSource.Play();
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
