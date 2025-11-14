using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeButtons : MonoBehaviour
{
    private int maxSpeedBonus = 5;
    private int maxShieldBonus = 3;
    private int maxDashBonus = 3;
    private int maxSizeBonus = 4;
    private int maxCawRangeBonus = 5;
    private float maxCawPercentageReduction = 35;
    
    private int speedBonus;
    private int shieldBonus;
    private int dashBonus;
    private int sizeBonus;
    private int cawRangeBonus;
    private float cawPercentageReduction;
    
    public TextMeshProUGUI speedBonusText;
    public TextMeshProUGUI shieldBonusText;
    public TextMeshProUGUI dashBonusText;
    public TextMeshProUGUI sizeBonusText;
    
    public TextMeshProUGUI cawPercentageText;
    public TextMeshProUGUI cawRangeBonusText;
    
    public TextMeshProUGUI amountOfUpgradesText;

    private void Awake()
    {
        RandomizeUpgradeBonuses();
        amountOfUpgradesText.text = "Upgrades: " + UpgradeManager.Instance.amountOfUpgrades;
        if (UpgradeManager.Instance.amountOfUpgrades < 1)
        {
            gameObject.GetComponent<Canvas>().enabled = true;
        }
    }

    private void RandomizeUpgradeBonuses()
    {
        speedBonus = Random.Range(1, maxSpeedBonus);
        shieldBonus = Random.Range(1, maxShieldBonus);
        
        if (Player2Controller.Instance != null)
        {
            dashBonus = Random.Range(1, maxDashBonus);
            sizeBonus = Random.Range(-5, maxSizeBonus);
        }else
        {
            cawPercentageReduction = Random.Range(0f, maxCawPercentageReduction);
            cawRangeBonus = Random.Range(0, maxCawRangeBonus);
        }
        ChangeButtontext();
    }

    public void ResetStats()
    {
        UpgradeManager.Instance.ResetStats();
    }

    public void ChangeButtontext()
    {
        speedBonusText.text = "+" + speedBonus + " Speed";
        shieldBonusText.text = "+" + shieldBonus + " Shield";
        
        if (Player2Controller.Instance != null)
        {
            dashBonusText.text = "+" + dashBonus + " Dash";
            if (sizeBonus < 0)
            { sizeBonusText.text = sizeBonus + " Size" + " +" +sizeBonus * -1 + " Speed"; } else
            { sizeBonusText.text = "+" + sizeBonus + " Size"; }
        }   
        else {
            cawPercentageText .text = "%" + cawPercentageReduction.ToString("F2") + " Caw Cooldown Reduction";
            cawRangeBonusText.text = "+" + cawRangeBonus + " Caw Range";
        }
    }
    public void AddSpeedBonus()
    {
        UpgradeManager.Instance._p1SpeedBonus += speedBonus;
        SceneTransitionManager.Instance.ReloadCurrentScene();
        UpgradeManager.Instance.amountOfUpgrades++;
    }

    public void AddSizeBonus()
    {
        UpgradeManager.Instance._p2SizeBonus += sizeBonus;
        if (sizeBonus < 0)
        {
            UpgradeManager.Instance._p2SpeedBonus += (sizeBonus * -1) * 2;
        }
        SceneTransitionManager.Instance.ReloadCurrentScene();
        UpgradeManager.Instance.amountOfUpgrades++;
    }

    public void AddShieldBonus()
    {
        UpgradeManager.Instance._p1Shield += shieldBonus;
        SceneTransitionManager.Instance.ReloadCurrentScene();
        UpgradeManager.Instance.amountOfUpgrades++;
    }
    public void AddCawRange()
    {
        UpgradeManager.Instance._p1cawRangeBonus += cawRangeBonus;
        SceneTransitionManager.Instance.ReloadCurrentScene();
        UpgradeManager.Instance.amountOfUpgrades++;
    }
    
    public void AddCawCooldownReduction()
    {
        UpgradeManager.Instance._p1CawCooldownReduction *= (1 - cawPercentageReduction/100);
        SceneTransitionManager.Instance.ReloadCurrentScene();
        UpgradeManager.Instance.amountOfUpgrades++;
    }

    public void AddDashAmount()
    {
        UpgradeManager.Instance._p2DashAmount += dashBonus;
        SceneTransitionManager.Instance.ReloadCurrentScene();
        UpgradeManager.Instance.amountOfUpgrades++;
    }
}
