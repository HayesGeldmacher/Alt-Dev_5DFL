using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{



    [HideInInspector] public bool _isPaused = false;
    [SerializeField] private CameraController _controller;
    [SerializeField] private TMP_Text _pausedText;
    [SerializeField] public GameObject _pauseButtons;
    [SerializeField] public GameObject _pauseButtonsText;
    [SerializeField] public Animator _pausedAnimator;
    [SerializeField] private AudioSource _pausedAudio;
    public GameObject _hudBorder;
    [SerializeField] private RawImage _cursorSprite;
    [SerializeField] private GameObject _ghostCam;


    [Header("Audio Variables")]
    [SerializeField] private float _fadeSpeed;
    private float _currentVolumeStatic;
    private float _currentVolumeDarkAmbience;
    private bool _isFading;
    [SerializeField] private AudioSource _staticAudio;
    [SerializeField] private AudioSource _darkAmbience;

    [SerializeField] private AudioSource _interactSound;

    [SerializeField] private TextGameManager _textGameManager;
    [SerializeField] private CardGameManager _cardGameManager;
    public bool _cardGame = false;
    public bool _inTextGame = false;

    [SerializeField] public TitleScreenSpriteFollowMouse _pauseCursor;
    [SerializeField] private TitleScreenSpriteFollowMouse _textCursor;

    public PauseButton pauseButtonScript;

    public delegate void CallPause();
    public static CallPause pauseInstance;

    public delegate void CallUnPause();
    public static CallUnPause unPauseInstance;





    //This singleton creates a locatable script instance that can be located easily from any other script!
    #region Singleton

    public static GameManager instance;

    void Awake()
    {

        if (instance != null)
        {
            Debug.Log("More than one instance of Manager is present! NOT GOOD!");
            return;
        }

        instance = this;


        pauseInstance += Pause;
        unPauseInstance += Unpause;

        if (Gamepad.all.Count > 0)
        {
            Debug.Log("Controller is being used!");
            PlayerPrefs.SetInt("device", 1);

        }
        else
        {
            Debug.Log("No controller detected!");
            PlayerPrefs.SetInt("device", 0);
        }
    }

    #endregion


    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChanged;
    }
    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChanged;
    }

    private void Start()
    {
        unPauseInstance.Invoke();
        _pauseCursor.EnableCursor(false);
        _pauseButtons.SetActive(false);
        if (_darkAmbience != null)
        {
            _currentVolumeDarkAmbience = _darkAmbience.volume;
        }

        if (_staticAudio != null)
        {
            _currentVolumeStatic = _staticAudio.volume;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {

            if (_isPaused)
            {

                unPauseInstance.Invoke();
            }
            else
            {
                pauseInstance.Invoke();
            }

            if (!_pausedAudio.isPlaying)
            {
                _pausedAudio.Play();
            }
        }

        if (_isFading)
        {
            if (_darkAmbience != null)
            {
                _currentVolumeDarkAmbience = Mathf.Lerp(_currentVolumeDarkAmbience, 0, _fadeSpeed * Time.deltaTime);
                _darkAmbience.volume = _currentVolumeDarkAmbience;
            }

            if (_staticAudio != null)
            {
                _currentVolumeStatic = Mathf.Lerp(_currentVolumeStatic, 0, _fadeSpeed * Time.deltaTime);
                _staticAudio.volume = _currentVolumeStatic;
            }
        }

    }
    // 1 = gamepad
    // 0 = keyboard
    private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                if (device is Gamepad)
                {
                    Debug.Log("Controller was added!");
                    PlayerPrefs.SetInt("device", 1);
                }
                break;
            case InputDeviceChange.Disconnected:
                if (device is Gamepad)
                {
                    Debug.Log("Controller was removed!");
                    PlayerPrefs.SetInt("device", 0);
                    if (!_isPaused) { pauseInstance.Invoke(); }
                }
                break;
            case InputDeviceChange.Removed:
                if (device is Gamepad)
                {
                    Debug.Log("Controller was removed!");
                    PlayerPrefs.SetInt("device", 0);
                    if (!_isPaused) { pauseInstance.Invoke(); }
                }
                // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                break;
            default:
                // See InputDeviceChange reference for other event types.
                break;
        }
    }

    public void LoadNextLevel()
    {
        _pauseCursor.EnableCursor(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void ReloadLevel()
    {
        _pauseCursor.EnableCursor(false);
        unPauseInstance.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        _pauseCursor.EnableCursor(false);
        unPauseInstance.Invoke();
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadSceneSpecific(string name)
    {
        _pauseCursor.EnableCursor(false);
        unPauseInstance.Invoke();
        SceneManager.LoadScene(name);
    }

    private void OnDestroy()
    {

        pauseInstance -= Pause;
        unPauseInstance -= Unpause;
        GameManager.instance = null;

    }



    public void FreezePlayer(bool _freeze)
    {
        Debug.Log("FUCKING STOPPED FREEZING BITCHES!");

        PlayerController.instance._frozen = _freeze;
        if (_controller != null)
        {
            _controller._frozen = _freeze;
        }
    }

    public void SetAudioBackgroundFade()
    {
        _isFading = true;
    }


    public void Pause()
    {


        _isPaused = true;
        if (_controller != null)
        {
            _controller.enabled = false;
        }
        Time.timeScale = 0f;

        if (!_inTextGame)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            _pauseCursor.EnableCursor(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        if (_textCursor != null && _inTextGame)
        {
            _textCursor.EnableCursor(false);
        }


        if (_controller != null)
        {
            if (_controller._hasCamera)
            {
                _pausedAnimator.SetBool("paused", true);
                _pausedText.text = "PAUSED";

            }

        }


        if (!_inTextGame)
        {
            _pauseButtons.SetActive(true);

        }
        _pauseButtonsText.SetActive(true);

        if (_hudBorder != null)
        {
            _hudBorder.SetActive(false);
        }

        if (_cursorSprite != null)
        {
            _cursorSprite.enabled = false;
        }

        if (_textGameManager != null)
        {
            _textGameManager.Pause();
        }

        if (_cardGame && _cardGameManager != null)
        {
            _cardGameManager.Pause();
        }
    }

    public void Unpause()
    {


        _isPaused = false;
        Time.timeScale = 1f;

        if (_controller != null)
        {
            _controller.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        if (!_inTextGame)
        {
            //THIS LINE CAUSES THE FUCKING PROBLEM!
            _pauseCursor.EnableCursor(false);

        }

        if (_textCursor != null)
        {
            if (_inTextGame)
            {
                _textCursor.EnableCursor(true);
            }
        }

        if (_controller != null)
        {
            if (_controller._hasCamera)
            {
                _pausedAnimator.SetBool("paused", false);
                _pausedText.text = "REC";

            }

        }

        _pauseButtons.SetActive(false);
        _pauseButtonsText.SetActive(false);

        if (pauseButtonScript != null)
        {
            pauseButtonScript.OnEscape();
        }





        if (_hudBorder != null)
        {
            _hudBorder.SetActive(true);
        }

        if (_cursorSprite != null)
        {
            _cursorSprite.enabled = true;
        }

        if (_textGameManager != null)
        {
            _textGameManager.UnPause();
        }

        if (_cardGame && _cardGameManager != null)
        {
            _cardGameManager.UnPause();
        }

    }


    public void PlayInteractSound()
    {
        _interactSound.pitch = Random.Range(0.8f, 1.2f);
        _interactSound.Play();
    }



    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // The home button was likely pressed, or the app is going into the background.
            Debug.Log("Application Paused (Home button pressed or app switched)");
            if (!_isPaused)
            {
                pauseInstance.Invoke();
            }
            // Add your game pause logic here
        }

    }
}
