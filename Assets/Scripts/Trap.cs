using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Scene management için

public class Trap : MonoBehaviour
{
    public string respawnPointName = "PlayerSpawn"; // Baþlangýç noktasý (Spawn) adý, Unity editoründe player için bir empty GameObject ekleyip buraya atayabilirsiniz.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Karakter tuzaða veya ateþe çarptý! Öldü!
            Debug.Log("Karakter tuzaða veya ateþe çarptý! Öldü!");

            // Karakteri hemen spawn noktasýna yerleþtiriyoruz
            RespawnPlayer(other.gameObject);

            // Sahneyi baþa döndürüyoruz
            StartCoroutine(ReloadScene());
        }
    }

    // Karakteri baþa döndürmek için bir fonksiyon
    private void RespawnPlayer(GameObject player)
    {
        // Player GameObject'inin respawnPoint'ine konumlandýrýyoruz.
        Transform respawnPoint = GameObject.Find(respawnPointName).transform;
        if (respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;  // Player'ý spawn noktasýna taþýyoruz.
        }
    }

    // Sahneyi yeniden yüklemek için coroutine
    private IEnumerator ReloadScene()
    {
        // Eðer bir animasyon veya geçiþ yapmak istiyorsanýz, buraya bekleme süresi ekleyebilirsiniz.
        yield return new WaitForSeconds(0.1f);  // Bu süreyi ihtiyaca göre deðiþtirebilirsiniz.

        // Sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükle
    }
}
