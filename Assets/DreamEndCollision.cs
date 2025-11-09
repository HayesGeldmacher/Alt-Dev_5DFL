using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DreamEndCollision : MonoBehaviour
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


    public Animator _blackAnim;
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("COLLIDEDWITHTRIGGA");
            if (!_entered)
            {
                _entered = true;
                StartCoroutine(EndScene());
            }
        }


    }

    private IEnumerator EndScene()
    {

        GameManager.instance.PlayInteractSound();
        
        if (_teleportPlayer && _player != null)
        {
            CharacterController _controller = _player.GetComponent<CharacterController>();
            _controller.enabled = false;
          //  _player.position = _spawnPos.position;
    
        }

        if (_eliminateAudioSource)
        {
            _crowdAudio.Stop();
            _dreamSong.Stop();
        }

        PlayerController playerController = _player.GetComponent<PlayerController>();
        //playerController._forcedForward = true;

        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
       // yield return new WaitForSeconds(1.5f);
        _blackAnim.SetTrigger("quick");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}


