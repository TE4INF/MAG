using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartmenyScript : MonoBehaviour
{

    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene("game");
        Time.timeScale = 1;
    }

    // Update is called once per frame
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
