using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject ESC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
         if(ESC.activeInHierarchy == false)
      {
        ESC.SetActive(true);
        Time.timeScale = 0;
      }
      else{
        ESC.SetActive(false);
        Time.timeScale = 1;
      }
        }
    }

    public void UnpauseButtonStyle()
    {
      if(ESC.activeInHierarchy == false)
      {
        ESC.SetActive(true);
        Time.timeScale = 0;
      }
      else{
        ESC.SetActive(false);
        Time.timeScale = 1;
      }
    }
}
