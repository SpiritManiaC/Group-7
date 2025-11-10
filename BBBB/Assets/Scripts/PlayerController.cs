using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]


public class PlayerController : MonoBehaviour
{
    #region "Movement"

    public static PlayerController Instance;
    public bool canMove = true;
    public float moveSpeed = 6f;
    public Vector2 moveDir;
    private PlayerInput _input;
    private int ShieldAmount    {
        get { return _shieldAmount;} set { _shieldAmount = value; }
    }
    public int _shieldAmount;

    public bool isMoving;

    #endregion

    
    private Rigidbody2D playerbody;


    private int CawRange    {
        get { return _cawRange;} set { _cawRange = value; }
    }
    public int _cawRange = 5;
    private bool Cawing    {
        get { return _cawing;} set { _cawing = value; }
    }

    private float cawCooldown = 3f;
    private float cawCooldownTimer;
    public bool _cawing;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        else
        {
            Destroy(gameObject);
        }
        _input = GetComponent<PlayerInput>();
        playerbody = GetComponent<Rigidbody2D>();
        moveSpeed += UpgradeManager.Instance._p1SpeedBonus;
        _shieldAmount = UpgradeManager.Instance._p1Shield;
    }

    private void Update()
    {
        UpdateMovement();
        
        if (Input.GetKeyDown(KeyCode.C) && cawCooldown > 0)
        {
            cawCooldown -= Time.deltaTime;
            SoundManager.Instance.PlaySFX(3);
            StartCoroutine(Caw());
        }
    }


    IEnumerator Caw()
    {
        List<GameObject> enemiesInRange = new List<GameObject>();
        List<GameObject> foodInRange = new List<GameObject>();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, CawRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                enemiesInRange.Add(hitCollider.gameObject);
            }
            if (hitCollider.gameObject.CompareTag("Food"))
            {
                foodInRange.Add(hitCollider.gameObject);
            }
        }
        foreach (GameObject enemy in enemiesInRange)
        {
            enemy.GetComponent<AiMovement>().canMove = false;
            yield return new WaitForSeconds(1f);
            enemy.GetComponent<AiMovement>().canMove = true;
        }
        foreach (GameObject food in foodInRange)
        {
            while (true)
            {
                food.transform.position = Vector2.MoveTowards(food.transform.position, transform.position,
                    20f * Time.deltaTime);
                yield return null;
            }
        }
        enemiesInRange.Clear();
        foodInRange.Clear();
        yield return new WaitForSeconds(2f);
    }
private void UpdateMovement()
{

    if (canMove)
    {
     if (_input.moveVector.x != 0 && Mathf.Abs(_input.moveVector.x) > Mathf.Abs(_input.moveVector.y))
     {
         moveDir.x = _input.moveVector.x;
       
     }
    
      if (_input.moveVector.y != 0 && Mathf.Abs(_input.moveVector.y) > Mathf.Abs(_input.moveVector.x))
       {
        moveDir.y = _input.moveVector.y;
   
      }


      if (Mathf.Abs(_input.moveVector.y) == Mathf.Abs(_input.moveVector.x))
     {
        
        moveDir.x = _input.moveVector.x;
       }

       if (_input.moveVector == Vector2.zero)
       {
           moveDir = Vector2.zero;
        }

        if (Mathf.Abs(moveDir.magnitude) > 0)
     {
        isMoving = true;
      }
      if (Mathf.Abs(moveDir.magnitude) !> 0)
      {
        isMoving = false;
      }
    }
    else
    { moveDir = Vector2.zero; }
    playerbody.linearVelocity = moveDir * moveSpeed;
}
}
