using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartmenyScript : MonoBehaviour
{
    public GameObject X1;
    public GameObject X2;
    public GameObject Audio1;


    // Start is called before the first frame update
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    public void Quit()
    {
       Debug.Log("Quit");
       Application.Quit();
    }

    public void trigger1()
    {
      if(X1.activeInHierarchy == false)
      {
        X1.SetActive(true);
        Audio1.SetActive(false);

      }
      else{
        X1.SetActive(false);
        Audio1.SetActive(true); 
      }
    }

    public void trigger2()
    {
        if(X2.activeInHierarchy == false)
        {
            X2.SetActive(true);
        }
        else{
            X2.SetActive(false);
        }

    }
}
