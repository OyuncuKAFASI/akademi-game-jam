using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public DefusedTrap defused;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("character"))
        {
            Destroy(gameObject);
            defused.Defuse();
            Destroy(defused.gameObject, 0.04f);
        }
    }
}
