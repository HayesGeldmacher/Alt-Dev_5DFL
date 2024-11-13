using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    [SerializeField] private Animator _flashIconAnim;
    [SerializeField] private Animator _lightAnim;
    [SerializeField] private AudioSource _flashSound;
    [SerializeField] private Light _flashLight;
    private bool _flashOn = false;
    private bool _canFlash = false;
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



            if (!_canFlash)
            {
               // _flashIconAnim.SetBool("present", true);
                _canFlash = true;
            }
            
            if (Input.GetMouseButtonDown(2) && !_handler._photoOpen)
            {

                _flashSound.Play();
                if (!_flashOn)
                {
                    _flashLight.enabled = false;
                    _flashOn = true;
                }
                else
                {
                    _flashLight.enabled = true;
                    _flashOn = false;
                }
                
                //   _currentFlashCharge = 0;
              //  _lightAnim.SetTrigger("flash");
               // _flashIconAnim.SetBool("present", false);
              //  _flashSound.Play();
             //   _canFlash = false;
            }

        if (Input.GetMouseButton(1) && !_handler._photoOpen)
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
}
