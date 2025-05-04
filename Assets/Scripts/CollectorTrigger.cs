using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorTrigger : MonoBehaviour
{
    public FireController fireController; // Ate�i kontrol edecek script
    public float fireDestroyDelay = 0.4f; // Ate�in yok olaca�� s�re (animasyon s�resi)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ate�i s�nd�rme animasyonunu ba�lat
            fireController.ExtinguishFire();

            // 0.4 saniye sonra ate�i yok et
            Destroy(fireController.gameObject, fireDestroyDelay);

            // Kolekt�r objesini yok et
            Destroy(gameObject);
        }
    }
}
