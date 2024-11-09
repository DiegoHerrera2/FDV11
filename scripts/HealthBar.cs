using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip getHealthSound;
    [SerializeField] private AudioClip getHurtSound;
    
    public Action<int> OnHealthChanged;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _audioSource = GetComponent<AudioSource>();
        OnHealthChanged += (newHealth) =>
        {
            var newHealth01 = newHealth / 100f;
            _audioSource.PlayOneShot(newHealth01 >= _slider.value ? getHealthSound : getHurtSound);
            _slider.value = newHealth01;
        };
    }
    
}
