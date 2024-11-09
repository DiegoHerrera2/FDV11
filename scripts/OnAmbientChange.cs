using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAmbientChange : MonoBehaviour
{
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioClip newAmbient;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("Changing ambient sound");
        ambientSource.clip = newAmbient;
        ambientSource.Play();
    }
}
