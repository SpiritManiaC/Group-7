using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeButtons : MonoBehaviour
{
    private int maxSpeedBonus = 3;
    private int maxShieldBonus = 2;
    private int maxDashBonus = 2;
    private int maxSizeBonus = 3;
    
    private int speedBonus;
    private int shieldBonus;
    private int dashBonus;
    private int sizeBonus;
    
    public TextMeshProUGUI speedBonusText;
    public TextMeshProUGUI shieldBonusText;
    public TextMeshProUGUI dashBonusText;
    public TextMeshProUGUI sizeBonusText;
    
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
        dashBonus = Random.Range(1, maxDashBonus);
        sizeBonus = Random.Range(-1, maxSizeBonus);
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
        dashBonusText.text = "+" + dashBonus + " Dash";
        if (sizeBonus < 0)
        { sizeBonusText.text = sizeBonus + " Size" + " +" +sizeBonus * -1 + " Speed"; } else
        { sizeBonusText.text = "+" + sizeBonus + " Size"; }
        
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

    public void AddDashAmount()
    {
        UpgradeManager.Instance._p2DashAmount += dashBonus;
        SceneTransitionManager.Instance.ReloadCurrentScene();
        UpgradeManager.Instance.amountOfUpgrades++;
    }
}
