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
    private EdgeCollider2D _edgeCollider2D;
    private BoxCollider2D _boxCollider2D;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _edgeCollider2D = GetComponent<EdgeCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
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
        _rigidbody2D.velocity = new Vector2(moveDirection * speed, _rigidbody2D.velocity.y);

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
