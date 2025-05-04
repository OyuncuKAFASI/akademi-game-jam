using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // Takip edilecek karakter
    public Vector3 offset;        // Kameranýn karaktere göre konum farký
    public float smoothSpeed = 0.125f;  // Kameranýn hareketini ne kadar yumuþatacaðýmýz
    public Vector2 minPosition;  // Kameranýn hareket etmeyeceði sýnýrlar (sol, alt)
    public Vector2 maxPosition;  // Kameranýn hareket etmeyeceði sýnýrlar (sað, üst)

    void LateUpdate()
    {
        // Kameranýn hedef pozisyonunu hesapla
        Vector3 desiredPosition = player.position + offset;

        // Sýnýrlandýrma iþlemi (kamera sadece belirtilen alan içinde hareket etsin)
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

        // Yumuþak hareket (lerp) ile kamerayý hareket ettir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamerayý yeni pozisyona taþý
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);  // Z-deðerini -10'da tut
    }
}
