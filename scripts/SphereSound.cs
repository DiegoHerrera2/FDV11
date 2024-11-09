using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSound : MonoBehaviour
{
    private AudioSource _source;
    private bool _moving = false;

    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;
        _source.loop = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _moving = true;
            _source.Play();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            _moving = false;
            _source.Stop();
        }
        
        if (_moving)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
    }
}
