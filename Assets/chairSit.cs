using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairSit : Interactable
{
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private Transform _newSpawnPos;
    [SerializeField] private Transform _camHolder;
    [SerializeField] private BoxCollider _playerBC;
    [SerializeField] private BoxCollider _chairBC;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private CameraController _camControl;
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private Transform _lookPoint;
    [SerializeField] private Transform _camHolderParent;
    [SerializeField] private bool _sat = false;
    [SerializeField] private bool _sittingAnim = false;
    [SerializeField] private bool _standingAnim = false;
    [SerializeField] private float _distance;
    [SerializeField] private AudioSource _chairAudio;
    [SerializeField] private AudioSource _chimesAudio;

    [Header("PreRequisite Variables")]
    [SerializeField] private bool _bulbOff = false;
    [SerializeField] private LightSwitch _lightSwitch;
    [SerializeField] private bool _doorClosed = false;
    [SerializeField] private Door _door;
    [SerializeField] private bool _flashOff = false;
    [SerializeField] CameraZoom _camZoom;
    public bool _firstInteracted = false;
    private bool _flashOffLastFrame = false;



    [SerializeField] private List<GameObject> _disappearObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _appearObjects = new List<GameObject>();


    [SerializeField] private float _totalSitTime;
    [SerializeField] private float _currentSitTime;

    [SerializeField] private GameObject _teddyBear;

    private bool _startedAudio = false;


    private void Start()
    {
        base.Start();
        gameObject.SetActive(false);
        _totalSitTime = _chairAudio.clip.length + 6;
    }

    public override void Interact()
    {
        if (!_firstInteracted)
        {
            _firstInteracted = true;
            
            if (!_sat)
            {
              if(_bulbOff && _doorClosed)
                {
                    EnterSit();
                }
                else
                {
                     base.Interact();
               
                }
            
            }
        }

    }

    private void EnterSit()
    {
        _camHolder.parent = null;
        _sittingAnim = true;
        PlayerController.instance._frozen = true;
        _charController.enabled = false;
        _playerBC.enabled = false;
        _chairBC.enabled = false;
        _sat = true;


        if (_flashOff)
        {
            StartCoroutine(StartAudioTrack());
        }
        else
        {
            base._dialogue._sentences[0] = "Flashlight off too, pal";
            base.Interact();
        }
        //_camControl._frozen = true;
    }

    private void Update()
    {
        _bulbOff = !_lightSwitch._on;
        _doorClosed = !_door._isOpen;
        _flashOff = _camZoom._flashOn;
        
        
        
        if (_sittingAnim)
        {
            _camHolder.position = Vector3.Lerp(_camHolder.position, _anchorPoint.position, 1 * Time.deltaTime);
            _camHolder.rotation = Quaternion.Lerp(_camHolder.rotation, _lookPoint.rotation, 1 * Time.deltaTime);

            _distance = Vector3.Distance(_camHolder.position, _anchorPoint.position);

            if(_distance <= 0.1f)
            {
                _sittingAnim = false;
                Debug.Log("ENDED SIT ANIMATION");
            }

        }
        else if(_standingAnim)
        {
             _camHolder.localPosition = Vector3.Lerp(_camHolder.localPosition, Vector3.zero, 1 * Time.deltaTime);
            _camHolder.localRotation = Quaternion.Lerp(_camHolder.localRotation, new Quaternion(0,0,0,0), 1 * Time.deltaTime);

            _distance = Vector3.Distance(_camHolder.localPosition, Vector3.zero);

            if (_distance <= 0.2f)
            {
               _standingAnim = false;
                Reattach2Body();

            }
        }

        if (_sat)
        {

            if (_flashOff)
            {
                _currentSitTime += Time.deltaTime;
                if(_currentSitTime > _totalSitTime)
                {
                    _sat = false;
                    StartCoroutine(FinishApparition());
                }
                else
                {
                    if (!_startedAudio)
                    {
                       StartCoroutine(StartAudioTrack());
                    }
                    else
                    {
                        if ((!_chairAudio.isPlaying) && (_chairAudio.time != 0))
                        {
                            _chairAudio.Play();
                        }
                    }

                    //checking to turn off dialogue!
                    if (_flashOffLastFrame == false)
                    {
                        EndDialogue();
                    }

                }
               

            }
            else
            {
                if (_startedAudio && _chairAudio.isPlaying)
                {
                    _chairAudio.Pause();
                }
            }
            
        }

        _flashOffLastFrame = _camZoom._flashOn;
    }


    private IEnumerator FinishApparition()
    {
        
        _teddyBear.SetActive(true);
        _chairAudio.Stop();
        _chimesAudio.Play();
        yield return new WaitForSeconds(2f);
        _sittingAnim = false;
        _camHolder.parent = _camHolderParent;
        _standingAnim = true;



    }

    private void Reattach2Body()
    {
        _camHolder.localRotation = new Quaternion(0, 0, 0, 0);
        _camHolder.localPosition = new Vector3(0, 0, 0);
        PlayerController.instance._frozen = false;
        _charController.enabled = true;
        _playerBC.enabled = true;
        _chairBC.enabled = true;
        TeleportPlayer();

    }
    public override void EndDialogue()
    {
        base._manager.EndDialogue();
    }

    private IEnumerator StartAudioTrack()
    {
        _startedAudio = true;
        yield return new WaitForSeconds(3);
        _chairAudio.Play();
    }

    private void TeleportPlayer()
    {
        AppearObjects();
        _playerParent.SetActive(false);
        //_controller._frozen = true;
        //_char.enabled = false;
        _playerParent.transform.position = _newSpawnPos.position;
        //Debug.Break();
        _playerParent.SetActive(true);
        PlayerController.instance._frozen = false;
        DisappearObjects();

    }


    private void AppearObjects()
    {

        foreach(GameObject _object in _appearObjects)
        {
            _object.SetActive(true);
        }
    }

    private void DisappearObjects()
    {
        foreach(GameObject _object in _disappearObjects)
        {
            _object.SetActive(false);
        }

    }

}
