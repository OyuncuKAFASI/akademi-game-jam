using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorTrigger : MonoBehaviour
{
    public FireController fireController; // Ateþi kontrol edecek script
    public float fireDestroyDelay = 0.4f; // Ateþin yok olacaðý süre (animasyon süresi)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ateþi söndürme animasyonunu baþlat
            fireController.ExtinguishFire();

            // 0.4 saniye sonra ateþi yok et
            Destroy(fireController.gameObject, fireDestroyDelay);

            // Kolektör objesini yok et
            Destroy(gameObject);
        }
    }
}
