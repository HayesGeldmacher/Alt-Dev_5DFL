using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{

    public bool _isOpen = false;
    public bool _isHeld = false;
    public bool _isLocked = false;

    [SerializeField] private bool _inFront = false;
    [SerializeField] private bool _hasInteracted = false;
    public Key _key;
    [SerializeField]  private bool _isSwinging = false;
    [SerializeField]  private bool _hasPlayedSound = false;
    [HideInInspector] public string _keyName;
    [SerializeField] private float _doorSensitivity = 5;
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private float _minYRot = -0.9f;
    [SerializeField] private float _maxYRot = 0.9f;
    private Transform _playerBody;
    private float _rotY;

    [Header("Audio")]
    [SerializeField] private AudioSource _lockedAudio;
    [SerializeField] private AudioSource _unlockedAudio;
    [SerializeField] private AudioSource _openedAudio;
    [SerializeField] private List<AudioClip> _doorCreaks = new List<AudioClip>();

    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (_key)
        {
        _keyName = _key.gameObject.name.ToString();

        }

        _playerBody = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (_isHeld)
        {
            //HoldUpdate();   
        }

        SwingUpdate();
    }
    
    public override void Interact()
    {
       if( _isLocked )
        {
            base.Interact();
            _lockedAudio.Play();
            CallEndDialogue();
        }
    }

    public void SetDirection()
    {
        if (_isLocked) return;
        
        Debug.Log("Direction Set!");
        _hasInteracted = true;
        Vector3 forward = transform.root.forward.normalized;
        Vector3 toOther = _playerBody.forward.normalized;
        float angle = Vector3.Angle(forward, toOther);
        Debug.Log(angle);

        if(angle < 90)
        {
            _inFront = true;
        }
        else
        {
            _inFront = false;
        }

        Open();
    }

    public void Open()
    {
        if (_isOpen)
        {
            _isOpen = false;
        }
        else
        {
            _isOpen = true;
        }

        _hasPlayedSound = false;
    }

    private void SwingUpdate()
    {
       
        //THis is the  big
        if (!_hasInteracted)
        {
                var zeroRotation = _doorPivot.localRotation;
                zeroRotation.x = 0;
                zeroRotation.z = 0;
                _doorPivot.localRotation = zeroRotation;
                _isSwinging = false;
            
        }
       
        else if (_isOpen)
        {

            if (_inFront)
            {
                if(Mathf.Abs(_doorPivot.localRotation.y - _minYRot) > 0.05f)
                {
                    _isSwinging = true;
                    _rotY = Mathf.Lerp(_doorPivot.localRotation.y, _minYRot, _doorSensitivity * Time.deltaTime);
                }
                else
                {
                    _isSwinging = false;
                }

            }
            else
            {
                if (Mathf.Abs(_doorPivot.localRotation.y - _maxYRot) > 0.05f)
                {
                    _isSwinging = true;
                    _rotY = Mathf.Lerp(_doorPivot.localRotation.y, _maxYRot, _doorSensitivity * Time.deltaTime);
                }
                else
                {
                    _isSwinging = false;
                }
            }

        }
        else
        {
            if (Mathf.Abs(_doorPivot.localRotation.y - 0) > 0.05f)
            {
                _isSwinging = true;
                 _rotY = Mathf.Lerp(_doorPivot.localRotation.y, 0, _doorSensitivity * Time.deltaTime);
            }
            else
            {
                _isSwinging = false;
            }

        }

        if (_isSwinging)
        {
            if (!_hasPlayedSound)
            {
                PlayOpenedAudio();
            }
        }

        if (_hasInteracted)
        {
        var newRotation = _doorPivot.localRotation;
        newRotation.x = 0;
        newRotation.z = 0;
        newRotation.y = _rotY;

        newRotation.y = Mathf.Clamp(newRotation.y, _minYRot, _maxYRot);
        _doorPivot.localRotation = newRotation;

        }

    }


    public override void CallEndDialogue()
    {
        base.CallEndDialogue();
    }

    public void Unlock()
    {
        _isLocked = false;
        _unlockedAudio.Play();
        
    }

    private void PlayOpenedAudio()
    {
        if (!_openedAudio.isPlaying)
        {
            int _randomClip = Random.Range(0, _doorCreaks.Count);
            _openedAudio.clip = _doorCreaks[_randomClip];
            
            _openedAudio.pitch = Random.Range(0.8f, 1.1f);
            _openedAudio.Play();
            _hasPlayedSound = true;
        }
    }
}
