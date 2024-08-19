using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    //private AudioSource _audioSource;
    private Animator _animator;

    private float _xInput;
    private float _jumpForce = 15f;
    [SerializeField] private int _size = 1;

    private int _obstaclesDestroyed = 0;

    private Vector2 _screenBounds;
    private CameraZoom _cameraZoom;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //_audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cameraZoom = Camera.main.gameObject.GetComponent<CameraZoom>();
        Jump();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        _xInput = Input.GetAxis("Horizontal");

        if (transform.position.y <= _screenBounds.y * -1 - 5)
        {
            //GameManager.Instance.GameOver();
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Grow();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_xInput * 8f, _rigidbody.velocity.y);
        transform.rotation = Quaternion.Euler(0, 0, _rigidbody.velocity.y);
    }

    private void LateUpdate()
    {
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
        _jumpForce++;
        _size++;
        if (_size % 5 == 0)
        {
            _animator.SetTrigger("OnMid");
        }
    }

    private void GoToNextState()
    {
        _cameraZoom.ZoomOut(5);
        _obstaclesDestroyed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Obstacle>() != null)
        {
            if (collision.transform.GetComponent<Obstacle>().IsDestroyedByCollision(_size))
            {
                _obstaclesDestroyed++;
                if (_obstaclesDestroyed >= 5)
                {
                    GoToNextState();
                }
            }

            //    GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Fruit>() != null)
        {
            Grow();
            Destroy(collision.gameObject);
        }
    }
}
