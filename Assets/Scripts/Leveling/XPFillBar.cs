using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPFillBar : MonoBehaviour
{
    public Slider xpSlider;
    public XPManager xpManager;

    // Update is called once per frame
    void Update()
    {
       if(xpSlider != null && xpManager != null)
        {
            float xpPrecent = (float)xpManager.currentXP / xpManager.CalculateXPRequiredForNextLevel();

            xpSlider.value = xpPrecent;
        }
        else
        {
            Debug.Log("ERROR ERROR");
        } 
    }

    public void ResetFillBar()
    {
        if(xpSlider != null)
        {
            Debug.Log("reset");
            xpSlider.value = 0f;
            xpManager.currentXP = 0;   
        }
    }
}
