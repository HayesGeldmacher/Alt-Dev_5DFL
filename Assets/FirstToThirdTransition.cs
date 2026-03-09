using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstToThirdTransition : MonoBehaviour
{
    public bool exitFirst = false;
    [SerializeField] private bool _entered = false;
    [SerializeField] private GameObject _playerFirstPerson;
    [SerializeField] private GameObject _playerThirdPerson;
    [SerializeField] private GameObject _playerFirstHUD;
    [SerializeField] private GameObject _playerThirdHUD;
    [SerializeField] private StartDataMosh _mosh;
    [SerializeField] private GameObject _SunBeams;

    [SerializeField] private float _lightValue;
    [SerializeField] private float _fogDensity;

    [SerializeField] private Camera _firstHUDCam;
    [SerializeField] private Camera _thirdHUDCam;
    [SerializeField] private Canvas _HUDCanvas;

    [Header("TeleportVariables")]
    [SerializeField] private protected bool _teleportPlayer;
    [SerializeField] private protected Transform _player;
    [SerializeField] private protected Transform _spawnPos;

    [SerializeField] protected bool _interact = false;
    public Interactable _interactable;

    [SerializeField] protected private CamChangePositions _camChange;
    [SerializeField] protected private int _camNum;

    [SerializeField] private GameObject _camOverlayHUD;
    public bool _playBreath = false;
    public bool _eliminateAudioSource = false;
    [SerializeField] private AudioSource _breatheAudio;
    [SerializeField] private AudioSource _crowdAudio;
    [SerializeField] private AudioSource _dreamSong;

    [Header("Dialogue Changes")]
    public bool changeDialogue = false;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Animator firstPersonTextBox;
    [SerializeField] private TMP_Text firstPersonDialogueText;
    [SerializeField] private GameObject hudDream;
    [SerializeField] private TitleScreenSpriteFollowMouse firstPersonCursor;
    [SerializeField] private GameObject pauseButtonsFirst;
    [SerializeField] private GameObject pauseButtonsTextFirst;
    [SerializeField] private Animator pausedAnimatorFirst;
    public TitleScreenSpriteFollowMouse firstPersonMouse;
    public CamChangePositions positions;
    public SingleMosh mosh;
    public bool playInteractSound = false;

    [Header("Settings Changes")]
    public PauseButton pauseButtonScript;




    [Header("FOG LIGHTING!")]
    public bool _fogLighting = false;
    public CamChangeLighting _fogChange;

    [Header("Audio Player Fields")]
    public bool fadeAudio = false;
    private bool isFading = false;
    public AudioSource audioToFade;
    public float fadeSpeed = 0.05f;

    private void Start()
    {
        //just for testing
        EnterFirstPerson(true);
    }


    private void Update()
    {
        if (isFading)
        {
            float oldVolume = audioToFade.volume;

            if (oldVolume <= 0)
            {
                isFading = false;
                return;
            }

            float newVolume = oldVolume - (fadeSpeed * Time.deltaTime);
            audioToFade.volume = newVolume;

        }
    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("COLLIDEDWITHTRIGGA");
            if (!_entered)
            {
                if (!exitFirst)
                {
                    EnterFirstPerson(true);
                    _entered = true;
                }
                else
                {
                    EnterFirstPerson(false);
                    _entered = true;
                }
            }
        }


    }

    private void EnterFirstPerson(bool first)
    {


        GameManager.instance.pauseButtonScript = pauseButtonScript;
        
        if (hudDream != null)
        {
            hudDream.SetActive(false);
        }
        if (changeDialogue)
        {
            if (dialogueManager != null)
            {
                if (firstPersonDialogueText != null)
                {
                    if (firstPersonTextBox != null)
                    {
                          dialogueManager._textAnim = firstPersonTextBox;
                          dialogueManager._dialogueText = firstPersonDialogueText;
                          GameManager.instance._pauseCursor = firstPersonCursor;
                        GameManager.instance._pauseButtons = pauseButtonsFirst;
                        GameManager.instance._pauseButtonsText = pauseButtonsTextFirst;
                        GameManager.instance._pausedAnimator = pausedAnimatorFirst;
                        //pauseButtonsFirst.GetComponent<PauseButton>().DisableCursor();

                    }
                }
            }
        }

        if (fadeAudio)
        {
            isFading = true;
        }


        if (_fogLighting)
        {
            if(_fogChange != null)
            {
                _fogChange.ChangeFogColor();
            }
        }
        
        
        if (first)
        {
        _playerFirstPerson.SetActive(true);
        _playerThirdPerson.SetActive(false);
            if (changeDialogue)
            {
                GameManager.pauseInstance.Invoke();
                GameManager.unPauseInstance.Invoke();
                //firstPersonMouse.EnableCursor(false);
                firstPersonMouse.CallKillMouseSprite();
                Debug.Log("invoked cursor Fade!");
                positions._mosh = mosh;
                if (playInteractSound)
                {
                    GameManager.instance.PlayInteractSound();
                }
            }

            if(_camOverlayHUD != null)
            {
             _camOverlayHUD.SetActive(true);
            }
            
            if(_HUDCanvas != null)
            {
                _HUDCanvas.worldCamera = _firstHUDCam;
            }

        }
        else
        {
            _playerThirdPerson.SetActive(true);
            _playerFirstPerson.SetActive(false);
            _camOverlayHUD.SetActive(false);
            _HUDCanvas.worldCamera = _thirdHUDCam;
            _camChange.ChangePos(_camNum);
        }
        if (_playBreath)
        {
        _breatheAudio.Play();

        }

        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();

        RenderSettings.ambientIntensity = _lightValue;
        RenderSettings.fogDensity = _fogDensity;

        _SunBeams.SetActive(false);
       

        if (_interact)
        {
            _interactable.Interact();
            GameManager.instance.PlayInteractSound();
        }

        if (_teleportPlayer && _player != null)
        {
            CharacterController _controller = _player.GetComponent<CharacterController>();
            _controller.enabled = false;
            _player.position = _spawnPos.position;
            _controller.enabled = true;
        }

        if (_eliminateAudioSource)
        {
            _crowdAudio.Stop();
            _dreamSong.Stop();
        }


    }

}
