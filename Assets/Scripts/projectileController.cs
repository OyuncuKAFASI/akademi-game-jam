using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 10;
    public string targetTag = "";
    private bool alreadyHit = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyHit) return;
        if (other.CompareTag(targetTag))
        {
            Debug.Log("VUR!");
            alreadyHit = true;
            if(targetTag == "character")
            {
                characterHealth targetHealth = other.GetComponent<characterHealth>();
                if (targetHealth != null)
                {
                    targetHealth.Hurt(damage);
                }
            }
            else if(targetTag == "enemy")
            {
                EnemyHealth targetHealth = other.GetComponent<EnemyHealth>();
                if (targetHealth != null)
                {
                    targetHealth.Hurt(damage);
                }
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
