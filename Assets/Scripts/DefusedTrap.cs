using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefusedTrap : MonoBehaviour
{
    public Animator anim;
    public Collider2D coll;
    
    public void Defuse()
    {
        if (anim != null)
        { 
            anim.SetTrigger("Defuse");
        }
        if (coll != null)
        { 
            coll.enabled = false;
        }
        Destroy(gameObject, 0.4f);
    }
}
