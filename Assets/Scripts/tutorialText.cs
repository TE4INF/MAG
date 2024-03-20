using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class tutorialText : MonoBehaviour
{
    [Header("Tutorial Text")]
    int tutorialAmount;
    int value = 0;
    [SerializeField] string[] tutorialTexts;
    [SerializeField] TextMeshProUGUI displayText;

    // [Header("Checks")]
    // [SerializeField] Transform player;
    // private Vector3 lastPosition;
    // float  movementTimer;
    // float moveTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        tutorialAmount = tutorialTexts.Length;
        displayText.text = tutorialTexts[value];
        // lastPosition = player.position;

    }

    // void Update()
    // {
    //     if (player.position != Vector3.zero)
    //     {
    //         movementTimer += Time.deltaTime;
    //         Debug.Log("Current time: " + movementTimer + " - Time to reach: " + moveTime);
    //     }

    //     if (movementTimer >= moveTime)
    //     {
    //         nextTutorial();
    //     }
    // }

    
    public void lastTutorial()
    {       
        value -= 1;
        displayText.text = tutorialTexts[value];
    }

    public void nextTutorial()
    {
        value += 1;

        if (value == tutorialAmount)
        {
            SceneManager.LoadScene("game");
                return;
        }

        if (value < tutorialAmount)
        {
            displayText.text = tutorialTexts[value];
        }

        
    }
}