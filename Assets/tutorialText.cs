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

    int tutorialAmount;
    int value = 0;
    public string[] tutorialTexts;
    public TextMeshProUGUI displayText;


    // Start is called before the first frame update
    void Start()
    {
        tutorialAmount = tutorialTexts.Length;
        displayText.text = tutorialTexts[value];
    }

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