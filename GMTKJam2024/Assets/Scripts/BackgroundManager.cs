using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance { get; private set; }

    private Vector3 _targetPosition;
    private float _velocity;
    private float _smoothTime = 2f;

    [SerializeField] private Transform _state1bg;
    [SerializeField] private Transform _state2bg;
    [SerializeField] private Transform _state3bg;

    private Vector3 _state1BasePos;
    private Vector3 _state2BasePos;
    private Vector3 _state3BasePos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _state1BasePos = _state1bg.position;
        _state2BasePos = _state2bg.position;
        _state3BasePos = _state3bg.position;
    }

    private void Update()
    {
        switch (GameManager.Instance.CurrentStateIndex)
        {
            // VAS Y NIQUE
            case 1:
                _targetPosition = Vector3.left * 0;
                break;
            case 2:
                _targetPosition = Vector3.left * 120;
                break;
            case 3:
                _targetPosition = Vector3.left * 720;
                break;
            default:
                break;
        }


        if (Vector3.Distance(transform.position, _targetPosition) > 0.05f)
        {
            float xPos = Mathf.SmoothDamp(transform.position.x, _targetPosition.x, ref _velocity, _smoothTime);
            if (Vector3.Distance(transform.position, _targetPosition) <= 0.1f)
            {
                transform.position = _targetPosition;
            }
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
    }


    public void SetBackgroundScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }

    public void ChangeStateBackground()
    {
        switch (GameManager.Instance.CurrentStateIndex)
        {
            case 1:
                _targetPosition = -_state1bg.position;
                break;
            case 2:
                _targetPosition = -_state2bg.position;
                break;
            case 3:
                _targetPosition = -_state3bg.position;
                break;
            default:
                break;
        }

    }

}
