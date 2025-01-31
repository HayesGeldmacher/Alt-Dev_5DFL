using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCam;
    [SerializeField] private Camera _cam;
    [SerializeField] private Camera _ghostCam;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;
    [SerializeField] private float _forwardZoomSpeed;
    [SerializeField] private float _backZoomSpeed;
    [SerializeField] private AudioSource _zoomAudio;
    [SerializeField] private ScreenshotHandler _handler;
    [SerializeField] private FlashilghtRot _flashLight;
    public bool _flashOn = false;
    private bool _canFlash = true;
    private float _currentZoom;
    [HideInInspector] public bool _isZooming;
    private bool _hasPlayedSound;
    private CameraController _camController;
    private float _currentFlashCharge = 0;


    private void Start()
    {
        //ensures both cameras start with same fov
        _currentZoom = _cam.fieldOfView;
        _ghostCam.fieldOfView = _currentZoom;
        _hasPlayedSound = false;
        _camController = transform.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (!_camController._hasCamera) { return; }



            if (Input.GetButtonDown("flash") && !_handler._photoOpen)
            {

                if (_canFlash)
                {

                 _flashLight.ChangeFlashStatus();
                }
            }


        bool _PressingTrigger;
        if (Input.GetAxis("Zoom") >= 0.1f || Input.GetButton("Zoom"))
        {
            _PressingTrigger = true;
        }
        else
        {
            _PressingTrigger= false;
        }

        if (_PressingTrigger && !_handler._photoOpen)
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
        _virtualCam.m_Lens.FieldOfView = _currentZoom;
    }

    public void TurnOffFlash()
    {

        if (_flashLight._active)
        {
            _flashLight.ChangeFlashStatus();
        }
       
    }

    public void TurnOnFlash()
    {
        if (!_flashLight._active)
        {
            _flashLight.ChangeFlashStatus();
        }
    }

    public bool CheckFlash()
    {
        return _flashLight._active;
    }
}
