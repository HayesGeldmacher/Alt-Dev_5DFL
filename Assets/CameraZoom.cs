using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera _cam;
    [SerializeField] private Camera _ghostCam;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    [SerializeField] private float _forwardZoomSpeed;
    [SerializeField] private float _backZoomSpeed;
    [SerializeField] private AudioSource _zoomAudio;
    private float _currentZoom;
    private bool _isZooming;
    private bool _hasPlayedSound;

    private void Start()
    {
        //ensures both cameras start with same fov
        _currentZoom = _cam.fieldOfView;
        _ghostCam.fieldOfView = _currentZoom;
        _hasPlayedSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _isZooming = true;
            
            if (!_zoomAudio.isPlaying && !_hasPlayedSound)
            {
                _zoomAudio.Play();
                _hasPlayedSound = true;
            }
        }
        else
        {
            _isZooming = false;
            _hasPlayedSound = false;
            if (_zoomAudio.isPlaying)
            {
                _zoomAudio.Stop();
            }
        }

        if (!_isZooming)
        {
            _currentZoom = Mathf.Lerp(_currentZoom, _maxZoom, _forwardZoomSpeed * Time.deltaTime);

        }
        else
        {
            _currentZoom = Mathf.Lerp(_currentZoom, _minZoom, _backZoomSpeed * Time.deltaTime);
            
        }

        

        _cam.fieldOfView = _currentZoom;
        _ghostCam.fieldOfView = _currentZoom;
    }
}