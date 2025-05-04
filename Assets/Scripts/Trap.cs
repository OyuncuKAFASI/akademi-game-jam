using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Scene management i�in

public class Trap : MonoBehaviour
{
    public string respawnPointName = "PlayerSpawn"; // Ba�lang�� noktas� (Spawn) ad�, Unity editor�nde player i�in bir empty GameObject ekleyip buraya atayabilirsiniz.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Karakter tuza�a veya ate�e �arpt�! �ld�!
            Debug.Log("Karakter tuza�a veya ate�e �arpt�! �ld�!");

            // Karakteri hemen spawn noktas�na yerle�tiriyoruz
            RespawnPlayer(other.gameObject);

            // Sahneyi ba�a d�nd�r�yoruz
            StartCoroutine(ReloadScene());
        }
    }

    // Karakteri ba�a d�nd�rmek i�in bir fonksiyon
    private void RespawnPlayer(GameObject player)
    {
        // Player GameObject'inin respawnPoint'ine konumland�r�yoruz.
        Transform respawnPoint = GameObject.Find(respawnPointName).transform;
        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;  // Player'� spawn noktas�na ta��yoruz.
        }
    }

    // Sahneyi yeniden y�klemek i�in coroutine
    private IEnumerator ReloadScene()
    {
        // E�er bir animasyon veya ge�i� yapmak istiyorsan�z, buraya bekleme s�resi ekleyebilirsiniz.
        yield return new WaitForSeconds(0.1f);  // Bu s�reyi ihtiyaca g�re de�i�tirebilirsiniz.

        // Sahneyi yeniden y�kle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden y�kle
    }
}
