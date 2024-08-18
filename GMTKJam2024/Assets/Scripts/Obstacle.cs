using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _sizeToDestroy;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_rigidbody.velocity.y) > 3f)
        {
            _collider.isTrigger = true;
        }
    }


    protected virtual void Move()
    {
        transform.Translate(Vector2.left * 8f * Time.deltaTime);

        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    public void Collide(int playerSize)
    {
        if (playerSize >= _sizeToDestroy)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
