using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StaticDreamManager : MonoBehaviour
{
    [Header("Shot Fields")]
    public StaticDreamPosition[] _shotPositions;
    public int _currentShot;
    public int _maxShot;
    public StartDataMosh _dataMosh;


    public Transform _player;


    [Header("CountDown Fields")]
    public float _clickCountDown = 2;
    private float _currentClickDown = 1;
    public bool _canClick = false;

    private AudioSource _interactAudio;

    [Header("End Fields")]
    public float _endTimeBuffer;
    public bool _hasEnded = false;
    public Animator _blackOut;
    public AudioFadeOut _audioFade;
    private bool _callGlitch = false;

    public Animator _cursorAnim;
    public RawImage _image;
    //The below region just creates a reference of this specific controller that we can call from other scripts quickly
    #region Singleton

    public static StaticDreamManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of playercontroller present!! NOT GOOD!");
            return;
        }

        instance = this;
    }

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
        GameManager.pauseInstance += DisablePointerImage;
        GameManager.unPauseInstance += EnablePointerImage;

    }

    private IEnumerator StartScene()
    {

        _blackOut.SetTrigger("blackInstant");
        _interactAudio = GetComponent<AudioSource>();
        _dataMosh.CallGlitch();
        Transform nextPosition = _shotPositions[0].transform;
        _player.localPosition = nextPosition.localPosition;
        _player.localRotation = nextPosition.localRotation;
        _currentClickDown = 4;
        yield return new WaitForSeconds(2f);
        _blackOut.SetTrigger("fade");
    }


    // Update is called once per frame
    void Update()
    {


        if (_canClick)
        {
            if (!_hasEnded)
            {
                if (!GameManager.instance._isPaused)
                {
                    if (Input.GetButtonDown("Interact"))
                    {
                        _canClick = false;
                        _currentClickDown = _clickCountDown;

                        CallNextShot();
                    }
                }

            }

        }
        else
        {
            _currentClickDown -= Time.deltaTime;
            if(_currentClickDown <= 0)
            {
                _canClick = true;
                _cursorAnim.SetBool("appear", true);
            }
        }

    }

    private void CallNextShot()
    {
        _interactAudio.Play();
        _shotPositions[_currentShot].CallPositionShot();
        _currentShot++;

        if(_currentShot >= _maxShot)
        {
            _hasEnded = true;
            StartCoroutine(EndScene());
        }
        else
        {
            _cursorAnim.SetTrigger("click");
            _cursorAnim.SetBool("appear", false);
        }
    }

    public void NextShot(Transform nextPosition)
    {
        if (_callGlitch)
        {
         _dataMosh.CallGlitch();
        }
        _player.localPosition = nextPosition.localPosition;
        _player.localRotation = nextPosition.localRotation;
        _callGlitch = true;
    }

    private IEnumerator EndScene()
    {
        _blackOut.SetTrigger("fade");
        _audioFade.StartFading();
        GameManager.pauseInstance -= DisablePointerImage;
        GameManager.unPauseInstance -= EnablePointerImage;
        yield return new WaitForSeconds(_endTimeBuffer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisablePointerImage()
    {
        _image.enabled = false;
    }

    public void EnablePointerImage()
    {
        _image.enabled = true;
    }

}
