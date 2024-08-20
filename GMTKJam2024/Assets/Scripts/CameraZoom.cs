using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _camera;

    private float _velocity = 0f;
    private float _smoothTime = 2f;

    private float _targetSize;

    private Vector2 _screenbounds;

    public Vector2 Screenbounds { get { return _screenbounds; } }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _targetSize = _camera.orthographicSize;
        _screenbounds = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
    }

    private void Start()
    {
        _camera.orthographicSize = 5;
        BackgroundManager.Instance.SetBackgroundScale(_camera.orthographicSize);
    }

    private void Update()
    {
        if (_camera.orthographicSize != _targetSize)
        {
            _camera.orthographicSize = Mathf.SmoothDamp(_camera.orthographicSize, _targetSize, ref _velocity, _smoothTime);
            if (Mathf.Abs(_camera.orthographicSize - _targetSize) <= 0.1f)
            {
                _camera.orthographicSize = _targetSize;
            }
            _screenbounds = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);

            BackgroundManager.Instance.SetBackgroundScale(_camera.orthographicSize);
        }
    }

    public void ZoomOut(float zoomValue)
    {
        _targetSize = _targetSize + zoomValue;
    }
}
