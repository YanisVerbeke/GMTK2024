using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int _sizeToDestroy;
    private Rigidbody2D _rigidbody;
    private PolygonCollider2D _collider;
    private CameraZoom _cameraZoom;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        _cameraZoom = Camera.main.gameObject.GetComponent<CameraZoom>();
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

        if (transform.position.y <= _cameraZoom.Screenbounds.y * -1 - 5 || transform.position.x <= _cameraZoom.Screenbounds.x * -1 - 5)
        {
            Destroy(gameObject);
        }
    }

    public bool IsDestroyedByCollision(int playerSize)
    {
        if (playerSize >= _sizeToDestroy)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            return true;
        }
        else
        {
            return false;
        }
    }
}
