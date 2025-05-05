using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    Animator animator;
    void Start()
    {
        animator  = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("HURT");
            animator.SetTrigger("hurt");
            Hurt(50);
        }
        if(health <=0)
        {
            Debug.Log("DEAD");
            animator.SetTrigger("dead");
            StartCoroutine(DisableAnimatorAtEnd());
        }
    }

    public void Hurt(float damage)
    {
        animator.SetTrigger("hurt");
        health-=50;
    }
    IEnumerator DisableAnimatorAtEnd()
    {
         float length = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(2*length);

        animator.enabled = false;

        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = false;
        }

        Destroy(gameObject);
    }
}
