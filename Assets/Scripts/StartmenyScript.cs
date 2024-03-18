using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartmenyScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("game");
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
