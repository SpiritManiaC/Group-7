using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public GameObject targetFood;
    public float moveSpeed;

    public bool canMove = true;
 //   public float distanceToPlayer;
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (ScoreManager.Instance.foodLeft.Count > 0)
            {
                if (!targetFood)
                {
                    NewTarget();
                }
                transform.position = Vector2.MoveTowards(transform.position, targetFood.transform.position,
                    moveSpeed * Time.deltaTime);
            
            }
            else
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
                    moveSpeed * -1 * Time.deltaTime);
            }
        }
      //  distanceToPlayer = Vector2.Distance(gameObject.transform.position, PlayerController.Instance.gameObject.transform.position);
  //      if (PlayerController.Instance._cawing && distanceToPlayer <= PlayerController.Instance._cawRange)
  //      { canMove = false;
    //    } else
    //    { canMove = true; }
        
    }

    public void NewTarget()
    {
        int targetRandom;
        targetRandom = Random.Range(0, ScoreManager.Instance.foodLeft.Count);
        targetFood = ScoreManager.Instance.foodLeft[targetRandom];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>()._shieldAmount > 0 
                                                  && ScoreManager.Instance.foodLeft.Count > 0)
        {
            SoundManager.Instance.PlayRandomEatSFX();
            other.gameObject.GetComponent<PlayerController>()._shieldAmount -= 1;
        } else if (other.gameObject.CompareTag("Player") && ScoreManager.Instance.foodLeft.Count > 0)
        {
            other.gameObject.GetComponent<SpawnOnDeath>().SpawnTombstone();
            SoundManager.Instance.PlayRandomEatSFX();
            Destroy(other.gameObject);
            GameStateManager.Instance.LoseGame();  
        } 
        if (other.gameObject.CompareTag("Player") && ScoreManager.Instance.foodLeft.Count <= 0)
        {
            gameObject.GetComponent<SpawnOnDeath>().SpawnTombstone();
            SoundManager.Instance.PlayRandomEatSFX();
            ScoreManager.Instance.enemiesLeft.Remove(gameObject);
            ScoreManager.Instance.LossOrWin();
            Destroy(gameObject);
        }
    }
}
