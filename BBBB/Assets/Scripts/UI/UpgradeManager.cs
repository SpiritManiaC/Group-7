using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private int P1SpeedBonus   {
        get { return _p1SpeedBonus;} set { _p1SpeedBonus = value; }
    }
    public int _p1SpeedBonus;
    
    private int P2SpeedBonus   {
        get { return _p2SpeedBonus;} set { _p2SpeedBonus = value; }
    }
    public int _p2SpeedBonus;
    
    private int P2SizeBonus    {
        get { return _p2SizeBonus;} set { _p2SizeBonus = value; }
    }
    public int _p2SizeBonus;
    
    private int P1Shield    {
        get { return _p1Shield;} set { _p1Shield = value; }
    }
    public int _p1Shield;
    
    private int P2DashAmount    {
        get { return _p2DashAmount;} set { _p2DashAmount = value; }
    }
    public int _p2DashAmount;
    public int amountOfUpgrades = 0;
    
    
    
    public static UpgradeManager Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }
    
    public void ResetStats()
    {
        P1SpeedBonus = 0;
        P2SizeBonus = 0;
        P1Shield = 0;
        P2DashAmount = 0;
        P1Shield = 0;
        P2DashAmount = 0;
    }
}
