using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{

    public bool _isHeld = false;
    public bool _isLocked = false;
    private bool _inFront = false;
    public Key _key;
    [HideInInspector] public string _keyName;
    [SerializeField] private float _doorSensitivity = 5;
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private float _minYRot = -0.9f;
    [SerializeField] private float _maxYRot = 0.9f;
    private Transform _player;
  

    [Header("Audio")]
    [SerializeField] private AudioSource _lockedAudio;
    [SerializeField] private AudioSource _unlockedAudio;
    [SerializeField] private AudioSource _openedAudio;

    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (_key)
        {
        _keyName = _key.gameObject.name.ToString();

        }

        _player = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (_isHeld)
        {
            HoldUpdate();   
        }
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
        Debug.Log("Direction Set!");
        
        Vector3 forward = transform.root.forward.normalized;
        Vector3 toOther = _player.forward.normalized;
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
    }

    private void HoldUpdate()
    {

        float _mouseY = Input.GetAxis("Mouse Y") * _doorSensitivity;

       if(_inFront)
        {
        _mouseY *= -1;

        }

        var newRotation = _doorPivot.localRotation;
        newRotation.x = 0;
        newRotation.z = 0;
        newRotation.y += _mouseY;
     
        newRotation.y = Mathf.Clamp(newRotation.y, _minYRot, _maxYRot);
        _doorPivot.localRotation = newRotation;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (_isHeld)
            {
            StartCoroutine(collision.transform.GetComponent<PlayerController>().MoveBack());

            }
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
}
