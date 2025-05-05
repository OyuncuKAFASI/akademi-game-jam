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
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float attackCooldown = 5f;
    private float lastAttackTime = -Mathf.Infinity;
    private bool isShooting = false;
    

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


        float targetVelocityX = moveDirection * speed;
        float velocityDiff = targetVelocityX - _rigidbody2D.velocity.x;
        float acceleration = grounded ? 10f : 5f; // slower air control

        _rigidbody2D.velocity += new Vector2(velocityDiff * acceleration * Time.fixedDeltaTime, 0);
        
        if (jump)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            jump = false;
        }
    }

    private void Update()
    {
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
        
        if (grounded && Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!IsInAnimationState("Attack") && Time.time >= lastAttackTime + attackCooldown)
            {
                anim.SetTrigger("attack");
                lastAttackTime = Time.time;
                isShooting = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
        while (collision.gameObject.CompareTag("Object"))
        {
            anim.SetBool("pushing", true);
            pushing = true;
        }
        pushing = false;
    }

    public void ShootProjectile()
    {
        if (isShooting) return; 
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float directionX = sr.flipX ? -1f : 1f;
        Vector2 direction = new Vector2(directionX, 0).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direction * projectileSpeed;
        
        SpriteRenderer projectileSR = projectile.GetComponent<SpriteRenderer>();
        if (projectileSR != null)
            projectileSR.flipX = directionX < 0;

        isShooting = true;
    }

    bool IsInAnimationState(string stateName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}