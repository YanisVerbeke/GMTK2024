using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    //private AudioSource _audioSource;
    private Animator _animator;
    private CapsuleCollider2D _collider;
    private SpriteRenderer _sprite;

    private float _xInput;
    private float _jumpForce = 15f;
    [SerializeField] private int _size = 1;

    private int _obstaclesDestroyed = 0;

    private Vector2 _screenBounds;
    private CameraZoom _cameraZoom;

    private int _lives = 3;
    private float _cooldownTimer = 0;
    private float _nextColorFlash = 0;
    [SerializeField] private Color _hurtColor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //_audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        _cameraZoom = Camera.main.gameObject.GetComponent<CameraZoom>();
        Jump();
        UiManager.Instance.DisplayLives(_lives);
    }

    private void Update()
    {
        if (transform.position.y <= _screenBounds.y * -1 - 5)
        {
            //GameManager.Instance.GameOver();
            Destroy(gameObject);
        }

        if (!GameManager.Instance.IsPlaying)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        _xInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.U))
        {
            Grow();
        }

        if (_cooldownTimer >= 0)
        {
            //_collider.isTrigger = true;
            _cooldownTimer -= Time.deltaTime;
            if (Time.time > _nextColorFlash)
            {
                ChangeColor();
                _nextColorFlash = Time.time + 0.2f;
            }
        }
        else
        {
            //_collider.isTrigger = false;
            _sprite.color = Color.white;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_xInput * 8f, _rigidbody.velocity.y);
        transform.rotation = Quaternion.Euler(0, 0, _rigidbody.velocity.y);
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        _screenBounds = _cameraZoom.Screenbounds;
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1, _screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y * -1 * 2, _screenBounds.y);
        transform.position = viewPos;
    }

    private void Jump()
    {
        //if (GameManager.Instance.IsPlaying == false)
        //    return;
        //_audioSource.PlayOneShot(_audioSource.clip);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _animator.SetTrigger("OnFlap");
    }

    private void Grow()
    {
        transform.localScale += Vector3.one * 0.5f;
        _rigidbody.mass += 0.1f;
        _jumpForce += 2;
        _size++;
        if (_size == GameManager.Instance.CurrentStateStats.sizeToNextState)
        {
            _animator.SetTrigger(GameManager.Instance.CurrentStateStats.nextAnimationName);
            Camera.main.gameObject.GetComponent<CameraShake>().StartShake();
        }
    }

    private void GoToNextState()
    {
        _cameraZoom.ZoomOut(GameManager.Instance.CurrentStateStats.nextZoomOutAmount);
        _obstaclesDestroyed = 0;
        _cooldownTimer = 3f;
        if (_lives < 3)
        {
            _lives++;
        }
        UiManager.Instance.DisplayLives(_lives);
        Camera.main.gameObject.GetComponent<CameraShake>().StopShake();
        GameManager.Instance.GoToNextState();
    }

    private void ChangeColor()
    {
        if (_sprite.color == _hurtColor)
        {
            _sprite.color = Color.white;
        }
        else
        {
            _sprite.color = _hurtColor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        if (collision.transform.GetComponent<Obstacle>() != null)
        {
            if (collision.transform.GetComponent<Obstacle>().IsDestroyedByCollision(_size))
            {
                _obstaclesDestroyed++;
                if (_obstaclesDestroyed >= GameManager.Instance.CurrentStateStats.objectToDestroy)
                {
                    GoToNextState();
                }
            }
            else
            {
                if (_cooldownTimer <= 0)
                {
                    if (collision.transform.GetComponent<Arrow>())
                    {
                        Destroy(collision.gameObject);
                    }

                    _lives--;
                    UiManager.Instance.DisplayLives(_lives);
                    _cooldownTimer = 3f;

                    if (_lives <= 0)
                    {
                        GameManager.Instance.GameOver();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        if (collision.transform.GetComponent<Fruit>() != null)
        {
            Grow();
            Destroy(collision.gameObject);
        }
    }
}
