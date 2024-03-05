using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject gameUI;
    // Start is called before the first frame update
    void Awake()
    {
        gameUI.SetActive(false);
        Debug.Log("pause");
        Invoke("tutorialend", 4f);
    }
    void tutorialend()
    {
        gameUI.SetActive(true);
        Debug.Log("unpause");
        Destroy(gameObject);
    }
}
