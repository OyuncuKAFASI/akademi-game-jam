using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class info_page : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    private AudioSource _audio;

    private int count = 0;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        if (_audio != null)
            _audio.Play();

        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            switch (count)
            {
                case 1:
                    text1.gameObject.SetActive(true);
                    break;
                case 2:
                    text2.gameObject.SetActive(true);
                    break;
                case 3:
                    text3.gameObject.SetActive(true);
                    break;
                case 4:
                    SceneManager.LoadScene(3);
                    break;
            }
        }
    }
}