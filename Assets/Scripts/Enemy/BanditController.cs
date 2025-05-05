using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public Transform player;
    private Animator animator;
    public enemyAttack weaponAttackScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        weaponAttackScript = GetComponentInChildren<enemyAttack>();
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
            animator.SetTrigger("attack");
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
            transform.localScale = new Vector3(1, 1, 1);
        else if (xDirection > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    bool IsInAnimationState(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public void ResetHit()
    {
        if (weaponAttackScript != null){
            weaponAttackScript.ResetHit();
        }
    }
}
