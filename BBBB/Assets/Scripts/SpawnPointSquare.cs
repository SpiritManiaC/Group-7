using UnityEngine;

public class SpawnPointSquare : MonoBehaviour
{
    private Collider2D spawnArea;

    public int foodToSpawn;
    public int enemiesToSpawn;
    
    public GameObject[] enemyList;
    public GameObject[] foodList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spawnArea = gameObject.GetComponent<Collider2D>();
        foodToSpawn = UpgradeManager.Instance.amountOfUpgrades * 5;
        enemiesToSpawn = UpgradeManager.Instance.amountOfUpgrades * 4;

        for (int j = 0; j < enemiesToSpawn; j++)
        {
            Vector2 randomPosition = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
            int enemySpawning = Random.Range(0, enemyList.Length);
            Instantiate(enemyList[enemySpawning], randomPosition, Quaternion.identity);
        }
        
        for (int j = 0; j < foodToSpawn; j++)
        {
            Vector2 randomPosition = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y));
            int foodSpawning = Random.Range(0, foodList.Length);
            Instantiate(foodList[foodSpawning], randomPosition, Quaternion.identity);
        }

        ScoreManager.Instance.UpdateFoodAndEnemyList();
    }

    // Update is called once per frame
}
