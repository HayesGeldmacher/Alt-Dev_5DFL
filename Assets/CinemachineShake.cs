using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    #region Singleton

    public static CinemachineShake instance;


    #endregion


    [SerializeField] private float _shakeTimer;
    [SerializeField] private float _diminishSpeed;
    public bool _isShaking;

    private float _startingIntensity;


    private CinemachineVirtualCamera _virtualCam;
    private float _shakeTimerTotal;

    private void Awake()
    {
        _virtualCam = GetComponent<CinemachineVirtualCamera>();

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of playercontroller present!! NOT GOOD!");
            return;
        }

        instance = this;

    }

    public void ShakeCamera(float _intensity, float _timer)
    {
        CinemachineBasicMultiChannelPerlin _virtualPerlin = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _virtualPerlin.m_AmplitudeGain = _intensity;
        _isShaking = true;
        _startingIntensity = _intensity;
        _shakeTimer = _timer;
        _shakeTimerTotal = _timer;
    }

    private void Update()
    {

        if (_isShaking)
        {
            if (_shakeTimer > 0)
            {
                _shakeTimer -= Time.deltaTime;

                CinemachineBasicMultiChannelPerlin _virtualPerlin = GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
                //CinemachineBasicMultiChannelPerlin _virtualPerlin = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                _virtualPerlin.m_AmplitudeGain = Mathf.Lerp(_startingIntensity, 0f, _diminishSpeed);

            }
            else
            {
                CinemachineBasicMultiChannelPerlin _virtualPerlin = GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
                //CinemachineBasicMultiChannelPerlin _virtualPerlin = _virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                _virtualPerlin.m_AmplitudeGain = 0;
                _isShaking = false;
            }

        }

    }
}
