using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    private bool _hasEncountered = false;
    bool _decreasing = false;
    bool _increasing = false;
    [SerializeField] private float _dayAmbience;
    public float _nightAmbience;
    private float _currentAmbience;
    private float _playerDistance;
    public Color _currentColor;
    public float totalSum;
    private bool _canCollide = true;

    //New lerp shit
    public Color _dayColor;
    public Color _nightColor;
    public Color _storedColor;
    public float _currentLerpValue;

    [Header("Speed Fields")]
    [SerializeField] private float _speed;
    public float _dayEndSpeed;

    public bool useFloatIntensity = false;
    public float minIntensity = 0.2f;
    public float maxIntensity = 0.2f;
    public float currentIntensity = 0.0f;

    private void Start()
    {
        _currentAmbience = _dayAmbience;
        _dayColor = RenderSettings.ambientLight;
        _currentColor = _dayColor;
        _currentLerpValue = 0;
        currentIntensity = RenderSettings.ambientIntensity;
        maxIntensity = RenderSettings.ambientIntensity;

}

    private void Update()
    {

        if (_decreasing)
        {
            if (useFloatIntensity)
            {
                currentIntensity = RenderSettings.ambientIntensity;
                currentIntensity -= (1 * _speed) * Time.deltaTime;
                RenderSettings.ambientIntensity = currentIntensity;
                if (currentIntensity <= minIntensity) {

                    _decreasing = false;
                    RenderSettings.ambientIntensity = minIntensity;
                }
                return;
            }

            _currentLerpValue += ((1 * _speed )* Time.deltaTime);
            _currentColor = Color.Lerp(_storedColor, _nightColor, _currentLerpValue);
            RenderSettings.ambientLight = _currentColor;
            if(_currentLerpValue >= 0.99f)
            {
                _decreasing = false;
                _currentLerpValue = 0;
            }
        }
        else if (_increasing)
        {
            if (useFloatIntensity)
            {
                currentIntensity = RenderSettings.ambientIntensity;
                currentIntensity += (1 * _speed) * Time.deltaTime;
                RenderSettings.ambientIntensity = currentIntensity;
                if (currentIntensity >= maxIntensity)
                {

                    _increasing = false;
                    RenderSettings.ambientIntensity = maxIntensity;
                }
                return;
            }


            _currentLerpValue += ((1 * _speed) * Time.deltaTime);
            _currentColor = Color.Lerp(_storedColor, _dayColor, _currentLerpValue);
            RenderSettings.ambientLight = _currentColor;
            if (_currentLerpValue >= 0.99f)
            {
                _increasing = false;
                _currentLerpValue = 0;
            }
        }



    }

    public void StartDecrease(float speed)
    {
        _speed = speed;
        _storedColor = RenderSettings.ambientLight;
        _increasing = false;
        _decreasing = true;
    }



    public void StartIncrease(float speed)
    {
        _speed = speed;
        _storedColor = RenderSettings.ambientLight;
        _decreasing = false;
        _increasing = true;
    }
  
}
