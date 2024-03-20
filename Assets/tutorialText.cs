using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class tutorialText : MonoBehaviour
{

    int tutorialAmount = 4;
    int value = 0;
    public string[] tutorialTexts;
    public TextMeshProUGUI displayText;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(tutorialTexts[value]);
        displayText.text = tutorialTexts[value];
    }

    public void lastTutorial()
    {
        value -= 1;
        displayText.text = tutorialTexts[value];
    }

    public void nextTutorial()
    {
        if (value <= tutorialAmount)
        {
            value += 1;
            displayText.text = tutorialTexts[value];
        }

    }
}