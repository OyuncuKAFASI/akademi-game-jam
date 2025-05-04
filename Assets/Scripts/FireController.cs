using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Animator fireAnimator;
    public Collider2D fireCollider;  // 2D uyumlu collider
    public float destroyDelay = 0.4f; // Ateşin yok olacağı süre (animasyon süresi)

    public void ExtinguishFire()
    {
        if (fireAnimator != null)
        {
            fireAnimator.SetTrigger("Extinguish"); // Animasyonu tetikle
        }

        if (fireCollider != null)
        {
            fireCollider.enabled = false; // Collider'ı kapat → karakter geçebilsin
        }

        // Animasyon süresi sonunda ateşi yok et
        Destroy(gameObject, destroyDelay);
    }
}
