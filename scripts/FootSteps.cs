using UnityEngine;

public class FootSteps : MonoBehaviour
{
    private AudioSource _source;
    [SerializeField] private float threshold = 1f;
    private Vector3 _lastPosition;
    void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;
        _lastPosition = transform.position;
    }

    void Update()
    {
        var distance = Vector3.Distance(_lastPosition, transform.position);
        
        if (distance > threshold)
        {
            _source.Play();
            _lastPosition = transform.position;
        }
    }
}