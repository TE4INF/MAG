using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPManager : MonoBehaviour
{
    public static XPManager main;
    [Header("Gameobjects")]
    public GameObject UpgradeUI;
    public GameObject PlayersUI;
    public TMP_Text Currency;
    public TMP_Text LevelIndicator;
    public XPFillBar xpFillBar;
    public Button HealthButton;
    public GameObject Lock;
    public GameObject WaveUI;
    [Header("PublicObjects")]
    public int baseXP = 100;
    public float XPScalingFactor = 1.2f;
    [Header("Infinate Mode")]
    public bool Infinate;
    [Header("values not to be touched")]
    public int currentXP = 0;
    private int currentLevel = 1;
    
    [SerializeField] private int UpgraedCurrancy = 0;

    private void Awake()
    {
        main = this;
        Debug.Log(CalculateXPRequiredForNextLevel());
        LevelIndicator.text = ("lvl: " + currentLevel);
    }

    public void CheckLevelUp()
    {
        if(UpgraedCurrancy > 0)
        {
            if(playerMovement.main.ShieldHealth == 1)
            {
              SetButtonActive(false);  
              Lock.SetActive(true);
            }
            else
            {
                SetButtonActive(true);
                Lock.SetActive(false);
            }
            WaveUI.SetActive(false);
            Currency.text = ("Level Points: " + UpgraedCurrancy);
            PlayersUI.SetActive(false);
            UpgradeUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void SetButtonActive(bool active)
    {
        HealthButton.interactable = active;
    }

    public void CalculateXP(string typeofenemy)
    {
        if(typeofenemy == "NormalEnemy")
        {
            currentXP += 5;
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

    public int CalculateXPRequiredForNextLevel()
    {
        return Mathf.RoundToInt(baseXP * Mathf.Pow(XPScalingFactor, currentLevel));
    }

    public void LevelUp()
    {
        UpgraedCurrancy++;
        currentLevel++;
        xpFillBar.ResetFillBar();
        LevelIndicator.text = ("lvl: " + currentLevel);
        if(Infinate == true)
        {
            CheckLevelUp();
        }
        //Animation or something
    }


    public void SpendCurrency()
    {
        UpgraedCurrancy --;
        Currency.text = ("Level Points: " + UpgraedCurrancy);
        if(UpgraedCurrancy == 0)
        {
            WaveUI.SetActive(true);
            UpgradeUI.SetActive(false);
            PlayersUI.SetActive(true);   
            Time.timeScale = 1;
        }    
    }

}
