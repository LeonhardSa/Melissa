using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
    }
    public void PlayGame()
    {
                
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
