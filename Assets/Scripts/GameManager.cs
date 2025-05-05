using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives = 3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied()
    {
        lives--;

        if (lives > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RestoreLives()
    {
        lives=3;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

    }
}
