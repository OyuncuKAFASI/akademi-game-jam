using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Takip edilecek karakter
    public Vector3 offset;        // Kameran�n karaktere g�re konum fark�
    public float smoothSpeed = 0.125f;  // Kameran�n hareketini ne kadar yumu�ataca��m�z
    public Vector2 minPosition;  // Kameran�n hareket etmeyece�i s�n�rlar (sol, alt)
    public Vector2 maxPosition;  // Kameran�n hareket etmeyece�i s�n�rlar (sa�, �st)

    void LateUpdate()
    {
        // Kameran�n hedef pozisyonunu hesapla
        Vector3 desiredPosition = player.position + offset;

        // S�n�rland�rma i�lemi (kamera sadece belirtilen alan i�inde hareket etsin)
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

        // Yumu�ak hareket (lerp) ile kameray� hareket ettir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kameray� yeni pozisyona ta��
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);  // Z-de�erini -10'da tut
    }
}
