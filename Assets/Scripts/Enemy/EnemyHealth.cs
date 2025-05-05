using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator  = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("HURT");
            animator.SetTrigger("hurt");
            Hurt();
        }
        if(health <=0)
        {
            Debug.Log("DEAD");
            animator.SetTrigger("dead");
            StartCoroutine(DisableAnimatorAtEnd());
        }
    }

    public void Hurt()
    {
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

        // // Bir süre sonra tamamen yok et (örneğin 2 saniye sonra)
        // yield return new WaitForSeconds(2f);
        Destroy(gameObject); // düşmanı sahneden kaldır
    }
}
