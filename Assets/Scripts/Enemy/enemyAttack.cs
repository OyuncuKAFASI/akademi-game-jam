using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public int damage = 20;
    private bool hasHit = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasHit && other.CompareTag("character"))
        {
            characterHealth playerHealth = other.GetComponent<characterHealth>();
            if (playerHealth != null)
            {
                playerHealth.Hurt(damage);
                hasHit = true;
            }
        }
    }

    public void ResetHit()
    {
        hasHit = false;
    }
}
