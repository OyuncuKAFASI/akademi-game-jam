using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Oyunu yeniden ba�lat�r (�u anki sahneyi yeniden y�kler)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // veya sahne ad�n� yaz: "OyunSahnesi"
    }

    // Oyunu kapat�r
    public void QuitGame()
    {
        Debug.Log("Oyundan ��k�l�yor...");
        Application.Quit();
    }
}
