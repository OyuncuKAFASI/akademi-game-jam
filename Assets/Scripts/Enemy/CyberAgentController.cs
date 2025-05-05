using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberAgentController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 0.25f;
    public Transform player;
    private Animator animator;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = -Mathf.Infinity;
    private bool isShooting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        Vector2 direction = (player.position - transform.position).normalized;

        if (distanceToPlayer <= attackRange)
        {
            animator.SetBool("isPlayerDetected", true);
            animator.SetBool("isPlayerNear", true);
            animator.SetBool("isRunning", false);
            FaceDirection(direction.x);

            if (!IsInAnimationState("Attack") && Time.time >= lastAttackTime + attackCooldown)
            {
                animator.SetTrigger("attack");
                lastAttackTime = Time.time;
                isShooting = false; // her saldırı başladığında tekrar ateş edebilmesi için sıfırla
            }
        }
        else if (distanceToPlayer <= detectionRange)
        {
            animator.SetBool("isPlayerNear", false);
            animator.SetBool("isPlayerDetected", true);
            animator.SetBool("isRunning", true);

            if (!IsInAnimationState("Attack"))
            {
                MoveTowards(direction);
            }
        }
        else
        {
            animator.SetBool("isPlayerDetected", false);
            animator.SetBool("isPlayerNear", false);
            animator.SetBool("isRunning", false);
        }
    }

    void MoveTowards(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        FaceDirection(direction.x);
    }

    void FaceDirection(float xDirection)
    {
        if (xDirection < 0)
            transform.localScale = new Vector3(-3, 3, 1);
        else if (xDirection > 0)
            transform.localScale = new Vector3(3, 3, 1);
    }

    bool IsInAnimationState(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    // Bu fonksiyon animasyondan çağrılmalı (Animation Event ile)
    public void ShootProjectile()
    {
        if (isShooting) return; // zaten ateş ettiyse bu saldırı döngüsünde bir daha etmesin

        Vector2 direction = (player.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direction * projectileSpeed;

        isShooting = true;
    }
}
