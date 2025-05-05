using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI bileşenleri için

public class characterHealth : MonoBehaviour
{
    public float health = 100;
    public Image[] heartImages; 
    public Sprite heartFull;  
    public Sprite heartHalf;   
    public Sprite heartEmpty; 
    public Image[] lifeIcons;
    public Sprite lifeFull;
    Animator animator;
    void Start()
    {
        animator  = GetComponent<Animator>();
        UpdateLifeUI();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     Debug.Log("HURT");
        //     Hurt(10);
        // }
        if(health <=0)
        {
            // Debug.Log("DEAD");
            animator.SetTrigger("death");
            StartCoroutine(DisableAnimatorAtEnd());
        }
    }

    public void Hurt(float damage)
    {
        animator.SetTrigger("hurt");
        health-=damage;
        UpdateHealthBar();
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

        GameManager.Instance.PlayerDied();

        Destroy(gameObject);
    }

    void UpdateHealthBar()
    {
        int heartsToShow = (int)(health / 20);
        int remainingHealth = (int)(health % 20);

        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].gameObject.SetActive(true);

            if (i < heartsToShow)
                heartImages[i].sprite = heartFull;
            else if (i == heartsToShow && remainingHealth > 0)
                heartImages[i].sprite = heartHalf;
            else
                heartImages[i].sprite = heartEmpty;
        }
    }

    public void UpdateLifeUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < GameManager.Instance.lives)
                lifeIcons[i].sprite = lifeFull;
            else
                lifeIcons[i].gameObject.SetActive(false);
        }
    }
}
