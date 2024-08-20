using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private float _targetDry = -10000;
    private float _targetReverb = 0;
    private float _velocity1;
    private float _velocity2;

    private AudioReverbFilter _reverb;

    private void Awake()
    {
        _reverb = GetComponent<AudioReverbFilter>();
        _reverb.dryLevel = 0;
        _reverb.reverbLevel = -10000;
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentStateIndex >= 4)
        {
            _reverb.dryLevel = Mathf.SmoothDamp(_reverb.dryLevel, _targetDry, ref _velocity1, 10f);
            _reverb.reverbLevel = Mathf.SmoothDamp(_reverb.reverbLevel, _targetReverb, ref _velocity2, 0.1f);
        }
    }
}
