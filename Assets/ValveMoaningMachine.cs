using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveMoaningMachine : Interactable
{

    private Quaternion _originalRot;
    private Vector3 _currentRot;
    private Quaternion _targetRot;
    [SerializeField] private float _targetZRot;

    [SerializeField] private bool _interacting;
    [SerializeField] private float _currentRotPower;
    [SerializeField] private float _rotPowerMult;

    [SerializeField] private CameraController _camControl;
    [SerializeField] private bool _rotating = false;
    [SerializeField] private AudioSource _valveAudio;
    private bool _playAudio = false;

    [SerializeField] private float _newRotz;
    private float _timeCount = 0;
    [SerializeField] private bool _finished = false;
    private bool _started = false;
    [SerializeField] private Quaternion _distance;
    [SerializeField] private Animator _valveAnim;
    [SerializeField] private GameObject _disappearObject;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _originalRot = transform.rotation;
        _distance = transform.rotation * Quaternion.Inverse(_targetRot);
        _targetRot = new Quaternion(_originalRot.x, _originalRot.y, _targetZRot, _originalRot.w);
        
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        if (_interacting)
        {
            if (Input.GetMouseButton(0) && !_finished)
            {

                RotateUpdate();

                if (!_started)
                {
                    _started = true;
                    _valveAudio.Play();
                }

            }
            else
            {
                _interacting = false;
                _camControl._frozen = false;
                PlayerController.instance._frozen = false;
                _rotating = false;
               
            }
        }
    }

    private void RotateUpdate()
    {
        float _rotatePowerHorizontal = Input.GetAxis("Mouse X") * Time.deltaTime;
        float _rotatePowerVertical = Input.GetAxis("Mouse Y") * Time.deltaTime;

        _currentRotPower = ((Mathf.Abs(_rotatePowerVertical) + Mathf.Abs(_rotatePowerHorizontal)) * _rotPowerMult);

        if(_currentRotPower > 0f)
        {
            _rotating = true;
        }
        else
        {
            _rotating = false;
        }

        if (_rotating)
        {
            // float _currentRotZ = transform.rotation.z;

            //float _newRotZ = _currentRotZ + (_currentRotPower * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _newRotZ);

            float speed = 0.005f;

            transform.rotation = Quaternion.Lerp(_originalRot, _targetRot, _timeCount * speed);
            _timeCount += Time.deltaTime;

          
        }

        _distance = transform.rotation * Quaternion.Inverse(_targetRot);
       if(Mathf.Abs(_distance.z) <= 100)
        {
            Debug.Log("WORKED!");
            _finished = true;
            Finish();
        }



    }

    private void Finish()
    {
        _interacting = false;
        _camControl._frozen = false;
        PlayerController.instance._frozen = false;
        _rotating = false;
        BoxCollider _bc = transform.GetComponent<BoxCollider>();
        _bc.enabled = false;
        _disappearObject.SetActive(false);
        _valveAnim.SetTrigger("disappear");
        this.enabled = false;

    }

    public override void Interact()
    {
        _interacting = true;
        _camControl._frozen = true;
        PlayerController.instance._frozen = true;
    }
}
