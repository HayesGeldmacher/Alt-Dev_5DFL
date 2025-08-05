using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("FOG LIGHTING!")]
    public bool _fogLighting = false;
    public CamChangeLighting _fogChange;


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
            _camOverlayHUD.SetActive(true);
        _HUDCanvas.worldCamera = _firstHUDCam;

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
