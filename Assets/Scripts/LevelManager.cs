using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.lives = 3;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

    }
}
