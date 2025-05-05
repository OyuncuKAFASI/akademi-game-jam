using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Oyunu yeniden baþlatýr (þu anki sahneyi yeniden yükler)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // veya sahne adýný yaz: "OyunSahnesi"
    }

    // Oyunu kapatýr
    public void QuitGame()
    {
        Debug.Log("Oyundan çýkýlýyor...");
        Application.Quit();
    }
}
