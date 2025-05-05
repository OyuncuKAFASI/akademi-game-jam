using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggTurretController : MonoBehaviour
{
    public float detectionRange = 5f;
    public Transform player;
    private Animator animator;
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;

    private bool isShooting = false;

    public float attackCooldown = 2f; // cooldown süresi
    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            FaceDirection(((player.position - transform.position).normalized).x);
            animator.SetBool("isAttacking", true);

            if (!isShooting && Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(ShootAfterAnim());
                lastAttackTime = Time.time;
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
            isShooting = false;
        }
    }

    void FaceDirection(float xDirection)
    {
        if (xDirection < 0)
            transform.localScale = new Vector3(2, 2, 1);
        else if (xDirection > 0)
            transform.localScale = new Vector3(-2, 2, 1);
    }

    IEnumerator ShootAfterAnim()
    {
        isShooting = true;

        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));

        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(length);

        ShootProjectile();

        isShooting = false; // tekrar atışa hazır hale getir
    }

    public void ShootProjectile()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direction * projectileSpeed;
    }
}
