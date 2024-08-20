using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private CameraZoom _cameraZoom;
    private float _moveSpeed = 5f;

    private void Start()
    {
        _cameraZoom = Camera.main.gameObject.GetComponent<CameraZoom>();
        transform.localScale = Vector3.one * Camera.main.orthographicSize / 5;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying)
            return;

        transform.Translate(Vector2.left * _moveSpeed * GameManager.Instance.CurrentStateIndex * Time.deltaTime);

        if (transform.position.x <= _cameraZoom.Screenbounds.x * -1 - 5)
        {
            Destroy(gameObject);
        }

        if (GameManager.Instance.CurrentStateIndex >= 4)
        {
            Destroy(gameObject);
        }
    }

}
