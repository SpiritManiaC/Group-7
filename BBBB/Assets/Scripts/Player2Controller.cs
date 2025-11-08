using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controller : MonoBehaviour
{
    public static Player2Controller Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool canDash = true;
    public int dashesLeft;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dashesLeft = UpgradeManager.Instance._p2DashAmount;
        Instance = this;
        gameObject.GetComponent<Transform>().localScale += 
            new Vector3(UpgradeManager.Instance._p2SizeBonus, UpgradeManager.Instance._p2SizeBonus);
    }

    public void OnMove1(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove");
        movementInput = context.ReadValue<Vector2>();
        
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        rb.linearVelocity = movementInput * (moveSpeed + UpgradeManager.Instance._p2SpeedBonus);
        if (Input.GetKeyDown(KeyCode.Space) &&  canDash && dashesLeft >= 1 )
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                dashesLeft -= 1;
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        rb.linearVelocity = movementInput * (moveSpeed * 100);
        yield return new WaitForSeconds(1f);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ScoreManager.Instance.enemiesLeft.Remove(other.gameObject);
            SoundManager.Instance.PlayRandomEatSFX();
            Destroy(other.gameObject);
            ScoreManager.Instance.AddPlayerScore(25);
        }

        if (other.gameObject.tag == "Player" && ScoreManager.Instance.foodLeft.Count == 0)
        {
            SoundManager.Instance.PlayRandomEatSFX();
            Destroy(gameObject);
            ScoreManager.Instance.LossOrWin();
        }
    }
}   
