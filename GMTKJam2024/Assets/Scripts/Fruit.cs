using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private CameraZoom _cameraZoom;

    private void Start()
    {
        _cameraZoom = Camera.main.gameObject.GetComponent<CameraZoom>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * 7f * Time.deltaTime);

        if (transform.position.x <= _cameraZoom.Screenbounds.x * -1 - 5)
        {
            Destroy(gameObject);
        }
    }

}
