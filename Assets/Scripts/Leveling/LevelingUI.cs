using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingUI : MonoBehaviour
{

    public void SpeedUpgrade()
    {
        playerMovement.main.UpgradeSpeed();
        levelingUp();
    }

    public void DamageUpgrade()
    {
        attack2.main.UpgradeDamage();
        levelingUp();
    }

    public void HealthUpgrade()
    {
        playerMovement.main.UpgradeHealth();
        levelingUp();
    }

    private void levelingUp()
    {
        XPManager.main.SpendCurrency();
    }
}
