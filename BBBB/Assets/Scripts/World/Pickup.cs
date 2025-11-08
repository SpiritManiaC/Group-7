using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[InfoHeaderClass("Drag me into the level to be picked up by the player :D")]
public class Pickup : MonoBehaviour
{
    /*
    [TextArea(1, 10)]
    [SerializeField]
    private string helpInfo = "Drag me into the level to be picked up by the player :D";
    */  
    [SerializeField, Header("How many points this pickup is worth")]
    private int value = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlayRandomEatSFX();
            ScoreManager.Instance.foodLeft.Remove(gameObject);
            other.gameObject.GetComponent<Transform>().localScale += new Vector3(0.2f, 0.2f);
            ScoreManager.Instance.AddPlayerScore(value);
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            ScoreManager.Instance.foodLeft.Remove(gameObject);
            ScoreManager.Instance.AddEnemyScore(value);
            Destroy(gameObject);
        }

    }
}
