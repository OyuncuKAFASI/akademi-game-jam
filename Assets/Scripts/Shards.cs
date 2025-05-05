using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shards : MonoBehaviour
{
    private GameObject newPortal;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        newPortal = GameObject.Find("Portal");
        
        if (newPortal != null)
            newPortal.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("character"))
        {
            if (_audio != null)
                _audio.Play();

            if (newPortal != null)
                newPortal.SetActive(true);

            Destroy(gameObject);
        }
    }
}


