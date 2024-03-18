using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skipTutorial : MonoBehaviour
{
    public void skip()
    {
        SceneManager.LoadScene("game");
        Time.timeScale = 1;
    }
}
