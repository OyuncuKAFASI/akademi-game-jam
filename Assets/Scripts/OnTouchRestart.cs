using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    private bool hasCollided = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character") && !hasCollided)
        {
            hasCollided = true;
            GameManager.Instance.PlayerDied();
        }
    }
}
