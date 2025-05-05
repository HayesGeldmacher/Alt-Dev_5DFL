using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    private bool _hasEncountered = false;
    bool _decreasing = false;
    bool _increasing = false;
    [SerializeField] private float _dayAmbience;
    [SerializeField] private float _nightAmbience;
    [SerializeField] private float _decreaseSpeed;
    private float _currentAmbience;
    private float _playerDistance;



    private bool _canCollide = true;

    private void Start()
    {
        _currentAmbience = _dayAmbience;
    }

    private void Update()
    {

        if (_decreasing)
        {
            _currentAmbience -= 1 * _decreaseSpeed * Time.deltaTime;
            // Debug.Log(_currentAmbience);
            RenderSettings.ambientIntensity = _currentAmbience;

            if (_currentAmbience < _nightAmbience)
            {
                _decreasing = false;
                _increasing = false;
            }
        }
        else if (_increasing)
        {
            _currentAmbience += 1 * _decreaseSpeed * Time.deltaTime;
            //Debug.Log(_currentAmbience);
            RenderSettings.ambientIntensity = _currentAmbience;

            if (_currentAmbience > _dayAmbience)
            {
                _increasing = false;
                _decreasing = false;
            }
        }



    }

    public void StartDecrease()
    {
        _decreasing = true;
        _increasing = false;
    }

    public void StartIncrease()
    {
        _decreasing = false;
        _increasing = true;
    }
  
}
