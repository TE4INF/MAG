using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    public static Display main;

    public TMP_Text kills;
    public TMP_Text Time;

    private void Awake()
    {
        main = this;
    }
    public void Dead()
    {
        kills.text = ("Your kill score is " + playerMovement.main.Kills);
        Time.text = ("You lasted in " + playerMovement.main.GetElapsedTime().ToString("F2") + " seconds");
    }
}
