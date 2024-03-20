using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartmenyScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1;
    }

    public void Endless()
    {
        SceneManager.LoadScene("Game Endless Mode");
        Time.timeScale = 1;
    }
    public void Quit()
    {
       Debug.Log("Quit");
       Application.Quit();
    }

    public void Startmeny()
    {
        SceneManager.LoadScene("Starting menu");
        Debug.Log("startmeny");
    }
}
