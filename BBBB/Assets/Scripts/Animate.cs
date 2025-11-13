using UnityEngine;

public class Animate : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 lastPosition;
    public float animationSpeed = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < lastPosition.x) // Moving left
        { sprite.flipX = true; } 
        else if (transform.position.x > lastPosition.x) // Moving right
        { sprite.flipX = false; }
        
        if (rb.linearVelocity.sqrMagnitude > 0.01f) // Use a small threshold to account for floating-point inaccuracies
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
        lastPosition = transform.position;
    }
}
