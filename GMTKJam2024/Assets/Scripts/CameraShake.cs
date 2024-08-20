using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    private float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    private float shakeAmount = 0.1f;
    private float decreaseFactor = 1.0f;

    private bool _contineousShake = false;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartShake();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopShake();
        }

        if (shakeDuration > 0 || _contineousShake)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount * GameManager.Instance.CurrentStateIndex;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void StartShake(float time = 0)
    {
        if (time == 0)
        {
            _contineousShake = true;
        }
        shakeDuration = time;
    }

    public void StopShake()
    {
        _contineousShake = false;
    }
}