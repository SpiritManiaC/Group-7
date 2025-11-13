using System;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    public GameObject objectToSpawn;

    private GameObject thisObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisObject = gameObject;
    }

    // Update is called once per frame
    public void SpawnTombstone()
    {
        GameObject tombStone = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        tombStone.transform.localScale = transform.localScale/1.5f;
    }
}
