using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    [SerializeField] private int healthPoints = 10;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        _audioSource.Play();
        other.gameObject.GetComponent<PlayerMovement>().OnCollectibleCollected.Invoke(healthPoints);
        Destroy(gameObject);
    }
}
