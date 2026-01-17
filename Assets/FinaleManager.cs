using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement; 
public class FinaleManager : MonoBehaviour
{
    [Header("Scene Timing Fields")]
    public float _introWait; //how long to fade in, should have audio immediately but fade in over seconds
    public float _openTime; //how long to stay open before fade again
    public float _closeTime; //how long after start fading to end game, go to credits

    public Animator _eyesAnim; //animator for eyes slowly close and fade away
    //add something here for camera can move limited!
    

    private bool _started = false;
    private bool _ended = false;

    private Interactable _dialogueInteract;

    public CRT _crt;


    [Header("Camera Zoom")]
    [SerializeField] private CinemachineVirtualCamera _virtualCam;
    [SerializeField] private float _endCamZoom;
    [SerializeField] private float _camZoomSpeed;
    [SerializeField] private bool _zooming = false;
    [SerializeField] private float _currentZoom;
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform _camMovePos;
    [SerializeField] private float _camFloatSpeed;



    [Header("Audio Fade")]
    [SerializeField] private AudioSource _ambientAudio;
    [SerializeField] private float _fadeSpeed;

    [SerializeField] private bool _fadingAudio = false;

    //scene lasts for 60 seconds, and then slowly fades out!
    // Start is called before the first frame update
    void Start()
    {

        //scene starts in black and then fades into existence
        _eyesAnim.SetTrigger("blackInstant");
        StartCoroutine(BeginScene());
        
    }

    // Update is called once per frame
    void Update()
    {



        if (_started)
        {
            if (!_ended)
            {
                _openTime -= Time.deltaTime;
                if (_openTime <= 0)
                {
                    _ended = true;
                    StartCoroutine(StartZoom());
                }
            }
        }


       

        if (_fadingAudio)
        {
            float currentVolume = _ambientAudio.volume;
            currentVolume -= (_fadeSpeed * (Time.deltaTime / 100));
            _ambientAudio.volume = currentVolume;
        }
    }

    private IEnumerator BeginScene()
    {
        _crt.StartBreathing();
        yield return new WaitForSeconds(_introWait);
        _eyesAnim.SetTrigger("fade");
        StartAudio();
        _started = true;
        yield return new WaitForSeconds(2f);
        _crt.EndBreathing();
        //allow cam to move limited
    }


    private IEnumerator StartZoom()
    {
        _zooming = true;
        yield return new WaitForSeconds(20f);
        _eyesAnim.SetTrigger("veryLong");
        yield return new WaitForSeconds(8f);
        _fadingAudio = true;
        yield return new WaitForSeconds(24f);
        StartCoroutine(EndScene());

    }



    private IEnumerator EndScene()
    {

      yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Credits");

    }


    private void StartAudio()
    {

    }

    private void EndAudio()
    {

    }

    private void LateUpdate()
    {
        if (_zooming)
        {
            _currentZoom = _virtualCam.m_Lens.FieldOfView;
            float newZoom = _currentZoom;
            newZoom -= (Time.deltaTime * _camZoomSpeed);



            if (_currentZoom <= _endCamZoom)
            {
                _zooming = false;
            }

            _virtualCam.m_Lens.FieldOfView = newZoom;



            //moving the camera!
            Vector3 currentPos = _cam.localPosition;
            Vector3 newPos = Vector3.Lerp(currentPos, _camMovePos.localPosition, _camFloatSpeed * Time.deltaTime);
            _cam.localPosition = newPos;
        }
    }
}
