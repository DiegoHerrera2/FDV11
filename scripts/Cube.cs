using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask sphereLayerMask;
    private Rigidbody _rb;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var inputx = Input.GetAxis("Horizontal");
        var inputy = Input.GetAxis("Vertical");
        
        _rb.velocity = new Vector3(inputx * speed, _rb.velocity.y, inputy * speed);
        
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if ((sphereLayerMask.value & 1 << other.gameObject.layer) == 0)
        {
            return;
        }
        // Change the volume of the audio source to match the speed of the cube
        _source.volume = other.relativeVelocity.magnitude / 10;
        Debug.Log("Volume: " + other.relativeVelocity.magnitude);
        _source.Play();
        
    }
}
