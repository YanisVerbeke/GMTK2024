using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Obstacle
{
    private bool _hasBeenShot = false;

    private void Update()
    {
        Quaternion rotation = Quaternion.LookRotation((Vector2)transform.position - rigidbody.velocity, transform.TransformDirection(Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        Move();
    }

    protected override void Move()
    {
        if (!_hasBeenShot)
        {
            base.rigidbody.AddForce(new Vector2(Random.Range(-2f, -1f), Random.Range(0.5f, 1.1f)).normalized * Random.Range(30f, 70f), ForceMode2D.Impulse);
            _hasBeenShot = true;
        }

        if (transform.position.y <= base.cameraZoom.Screenbounds.y * -1 - 5 || transform.position.x <= base.cameraZoom.Screenbounds.x * -1 - 5)
        {
            Destroy(gameObject);
        }
    }
}
