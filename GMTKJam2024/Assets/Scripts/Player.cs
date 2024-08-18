using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    //private AudioSource _audioSource;

    private float _xInput;
    private float _jumpForce = 15f;
    private int _size = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //_audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Jump();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        _xInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -10)
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

    private void Jump()
    {
        //if (GameManager.Instance.IsPlaying == false)
        //    return;
        //_audioSource.PlayOneShot(_audioSource.clip);
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void Grow()
    {
        transform.localScale += Vector3.one * 0.5f;
        _rigidbody.mass += 0.1f;
        _jumpForce++;
        _size++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<Obstacle>() != null)
        {
            collision.transform.GetComponent<Obstacle>().Collide(_size);

            //    GameManager.Instance.GameOver();
        }
    }
}
