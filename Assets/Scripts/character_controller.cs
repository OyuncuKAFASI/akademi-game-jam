using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float jumpForce = 3.0f;
    public float speed = 7.0f;
    private float moveDirection;

    private bool jump;
    private bool grounded = true;
    private bool pushing = false;
    private bool moving;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator anim;
    

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D.velocity != Vector2.zero)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        // Apply movement and keep vertical velocity
        float targetVelocityX = moveDirection * speed;
        float velocityDiff = targetVelocityX - _rigidbody2D.velocity.x;
        float acceleration = grounded ? 10f : 5f; // slower air control

        _rigidbody2D.velocity += new Vector2(velocityDiff * acceleration * Time.fixedDeltaTime, 0);

        // Apply jump
        if (jump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            jump = false;
        }
    }

    private void Update()
    {
        // Allow movement input even in mid-air
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1.0f;
            _spriteRenderer.flipX = true;
            anim.SetFloat("speed", speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1.0f;
            _spriteRenderer.flipX = false;
            anim.SetFloat("speed", speed);
        }
        else
        {
            moveDirection = 0.0f;
            anim.SetFloat("speed", 0.0f);
        }

        // Only allow jumping when grounded
        if (grounded && Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
        else if (collision.gameObject.CompareTag("Object"))
        {
            anim.SetBool("pushing", true);
            pushing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            anim.SetBool("pushing", false);
            pushing = false;
        }
    }
}