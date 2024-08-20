using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int _sizeToDestroy;
    protected new Rigidbody2D rigidbody;
    protected new PolygonCollider2D collider;
    protected CameraZoom cameraZoom;
    private float _moveSpeed = 6f;
    protected bool isDestroyed = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        cameraZoom = Camera.main.gameObject.GetComponent<CameraZoom>();
        _sizeToDestroy = GameManager.Instance.CurrentStateStats.sizeToNextState;
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rigidbody.velocity.y) > 3f && isDestroyed)
        {
            collider.isTrigger = true;
        }
    }


    protected virtual void Move()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        transform.Translate(Vector2.left * _moveSpeed * GameManager.Instance.CurrentStateIndex * Time.deltaTime);

        if (transform.position.y <= cameraZoom.Screenbounds.y * -1 - 5 || transform.position.x <= cameraZoom.Screenbounds.x * -1 - 5)
        {
            Destroy(gameObject);
        }
    }

    public bool IsDestroyedByCollision(int playerSize)
    {
        if (playerSize >= _sizeToDestroy)
        {
            isDestroyed = true;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            return true;
        }
        else
        {
            return false;
        }
    }
}
