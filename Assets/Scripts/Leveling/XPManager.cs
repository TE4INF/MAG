using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public static XPManager main;
    public GameObject UpgradeUI;
    public GameObject PlayersUI;
    public TMP_Text Currency;

    public int baseXP = 100;
    public float XPScalingFactor = 1.2f;

    private int currentRound = 0;
    private int currentXP = 0;
    private int currentLevel = 1;
    
    [SerializeField] private int UpgraedCurrancy = 0;

    private void Awake()
    {
        main = this;
        Debug.Log(CalculateXPRequiredForNextLevel());
    }

    public void CheckLevelUp()
    {
        if(UpgraedCurrancy > 0)
        {
            Currency.text = ("Level Points: " + UpgraedCurrancy);
            PlayersUI.SetActive(false);
            UpgradeUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CalculateXP(string typeofenemy)
    {
        if(typeofenemy == "NormalEnemy")
        {
            currentXP += 50;
            if (currentXP >= CalculateXPRequiredForNextLevel())
        {
            LevelUp();
        }
        }
        else if(typeofenemy == "ArcherEnemy")
        {
            currentXP += 10;
            if (currentXP >= CalculateXPRequiredForNextLevel())
        {
            LevelUp();
        }
        }
        else if(typeofenemy == "Boss")
        {
            currentXP += 20;
            if (currentXP >= CalculateXPRequiredForNextLevel())
        {
            LevelUp();
        }
        }
    }

    private int CalculateXPRequiredForNextLevel()
    {
        return Mathf.RoundToInt(baseXP * Mathf.Pow(XPScalingFactor, currentLevel));
    }

    public void LevelUp()
    {
        UpgraedCurrancy++;
        currentLevel++;
        //Animation or something
    }

    public void SpendCurrency()
    {
        UpgraedCurrancy --;
        Currency.text = ("Level Points: " + UpgraedCurrancy);
        if(UpgraedCurrancy == 0)
        {
            UpgradeUI.SetActive(false);
            PlayersUI.SetActive(true);   
            Time.timeScale = 1;
        }    
    }

}
