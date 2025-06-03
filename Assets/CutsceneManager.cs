using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{

    [SerializeField] private Animator _screenAnim;
    [SerializeField] private Animator _vidAnim;

    [SerializeField] private VideoPlayer _vid;
    [SerializeField] private AudioSource _carAmbience;
    [SerializeField] private AudioSource _infomercialSound;


    [SerializeField] private Animator _clickAnim;
    private bool _canStart = false;
    private bool _hasStarted = false;
    [SerializeField] private AudioSource _interactSound;
    [SerializeField] private AudioSource _doorSound;

    [Header("Static Audio Fade")]
    [SerializeField] private AudioSource _staticAudio;
    private float _startingVol;
    [SerializeField] private float _fadeSpeed;
    private bool _fading = false;
    private float _currentVol;
    [SerializeField] private bool _returnToMenu = false;
    private bool _doorAppeared = false;

    public bool _endEarly = false;

    [Header("PeanutSequence")]
    [SerializeField] private float _peanutWatchTime;
    [SerializeField] private Animator _blackScreen;
    // Start is called before the first frame update
   public void Begin()
    {

        
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _startingVol = _staticAudio.volume;
        _currentVol = _startingVol;
        StartCoroutine(StartPeanutSequence());
    }

    public void BeginDoor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _startingVol = _staticAudio.volume;
        _currentVol = _startingVol;
        StartCoroutine(DoorIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (!_hasStarted && _doorAppeared)
            {
                _hasStarted = true;
                StartSequence();
            }
        }

        if (_fading)
        {
          //   _currentVol = Mathf.Lerp(_currentVol, 0, _fadeSpeed * Time.deltaTime);
          //  _staticAudio.volume = _currentVol;
            
        }
    }



    private IEnumerator StartPeanutSequence()
    {
        _blackScreen.SetTrigger("black");
        yield return new WaitForSeconds(1.5f);
        _screenAnim.SetTrigger("black");
        _infomercialSound.Play();
        yield return new WaitForSeconds(0.5f);
        _screenAnim.SetTrigger("start");
        yield return new WaitForSeconds(_peanutWatchTime);
        EndPeanutSequence();
        
    }

    private void EndPeanutSequence()
    {
        _screenAnim.SetTrigger("end");
       StartCoroutine(StartRoadTripSequence());
    }

    private IEnumerator StartRoadTripSequence()
    {
         yield return new WaitForSeconds(5f);
        _vidAnim.SetTrigger("road");
        _vid.Play();
        _carAmbience.Play();

    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(1.5f);
        if (!_hasStarted)
        {
            _canStart = true;
        }
    }

    private void StartSequence()
    {
        _vidAnim.SetTrigger("start");
        _clickAnim.SetTrigger("click");
        _doorSound.Play();
        _interactSound.Play();
    }

    public void LoadNextScene()
    {
        if (_returnToMenu) 
        {   
          LoadMainMenu();
        }
        else
        {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void KillVisualNoise()
    {
        _fading = true;
    }

    public void CallDoorIntro()
    {
        if (!_doorAppeared)
        {
            StartCoroutine(DoorIntro());
        } 
    }

    private IEnumerator DoorIntro()
    {
        if (_endEarly)
        {
            yield return new WaitForSeconds(2);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {

            _vidAnim.SetTrigger("door");
       yield return new WaitForSeconds(1f);
        
        _doorAppeared = true;
        _clickAnim.SetTrigger("appear");

        }

    }


}
