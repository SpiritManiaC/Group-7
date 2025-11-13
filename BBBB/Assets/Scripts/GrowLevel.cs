using UnityEngine;

public class GrowLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.GetComponent<Transform>().localScale += new Vector3(UpgradeManager.Instance.amountOfUpgrades,UpgradeManager.Instance.amountOfUpgrades);
    }
}
