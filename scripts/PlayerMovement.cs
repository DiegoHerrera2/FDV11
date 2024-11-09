using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthBar healthBar;
    
    private State _state = State.Idle;
    private Rigidbody2D _rb;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float thrust = 30.0f;
    private int _lastMoveX;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;
    
    private bool _isGrounded;
    private bool _jumpPressed;
    private int _health = 100;
    
    public Action<int> OnCollectibleCollected;
    
    private enum State
    {
        Idle,
        Walk
    }
    
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        
        OnCollectibleCollected += (healthPoints) =>
        {
            _health = Math.Clamp(_health + healthPoints, 0, 100);
            healthBar.OnHealthChanged.Invoke(_health);
        };
    }

    // Update is called once per frame
    private void Update()
    {
        var x = Input.GetAxisRaw("Horizontal");
        
        animator.SetFloat(Horizontal, x);
        switch (_state)
        {
            case State.Idle:
                animator.SetBool(IsWalking, false);
                if (x != 0)
                {
                    _state = State.Walk;
                }
                break;    
            case State.Walk:
                animator.SetBool(IsWalking, true);
                spriteRenderer.flipX = _lastMoveX > 0;
                _lastMoveX = x > 0 ? 1 : -1;
                if (x == 0)
                {
                    _state = State.Idle;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpPressed = true;
        }
    }
    private void FixedUpdate()
    {
        var x = Input.GetAxisRaw("Horizontal");

        var velocity = new Vector2(x * speed * Time.fixedDeltaTime, _rb.velocity.y);

        _rb.velocity = velocity;
        
        CheckGrounded();
        
        if (_jumpPressed && _isGrounded)
        {
            _rb.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(jumpSound);
            _isGrounded = false;
            _jumpPressed = false;
        }
    }
    
    private void CheckGrounded()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f);
        if (!_isGrounded && hit.collider is not null)
        {
            _audioSource.PlayOneShot(landSound);
            _isGrounded = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(other.transform);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}
