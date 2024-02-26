using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //the below variables check wether the player is touching the ground or not!
    [Header("Grounded Variables")]
    private bool _grounded;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _gravity;

    [Header("Walking Variables")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private Animator _camAnim;

    [Header("Crouch Variables")]
    [SerializeField] private Transform _cameraParent;
    [SerializeField] private float _crouchSpeed = 6;
    [SerializeField] private float _crouchTransSpeed;
    [SerializeField] private float _crouchingYCamPoint = 0;
    private bool _isCrouching;
    private float _yCamPoint = 0;
    private float _standingYCamPoint = 0;

    [Header("Interaction Variables")]
    [SerializeField] private Transform _backPoint;
    [SerializeField] private float _moveBackSpeed;
    private bool _isMovingBack = false;
    private Vector3 _moveBackTarget;
    [SerializeField] private GameObject _light;
    [SerializeField] private SoundManager _soundManager;
   
    


    //The below region just creates a reference of this specific controller that we can call from other scripts quickly
    #region Singleton

    public static PlayerController instance;

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

    private void Start()
    {
        //Getting a reference for where the camera should be when standing
        _standingYCamPoint = _cameraParent.transform.localPosition.y;

        //Disables the player capsule mesh so we dont see it during playtime!
        MeshRenderer _mesh = GetComponent<MeshRenderer>();
        _mesh.enabled = false;
    }

    private void Update()
    {
        if (_isMovingBack)
        {
            MoveBackUpdate();
        }
        else
        {

            //This line checks if the player is touching the ground, or in the air
            _grounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            //takes the raw player input to move character 
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            //Stores that input in a variable to be used later in function
            Vector3 _move = transform.right * x + transform.forward * z;

           //Constantly adding a downward force to the player so they fall when not standing on something
            _velocity.y += _gravity * Time.deltaTime;

            float _speed;
            if (_isCrouching)
            {
                _speed = _crouchSpeed;
            }
            else
            {
                _speed = _walkSpeed;
            }

           //controller.move is how the character actually moves - always multiply by Time.deltaTime so physics work correctly!
            _controller.Move(_move * _speed * Time.deltaTime);
            _controller.Move(_velocity * Time.deltaTime);

            //Controls whether the camera is animating to walk or not
            if(_move.magnitude > 0.1f) 
            {
                _camAnim.SetBool("walking", true);
            }
            else
            {
                _camAnim.SetBool("walking", false);
            }

            //This lines allows the player to turn their flashlight on or off!
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (_light.active)
                {
                    _light.SetActive(false);
                }
                else
                {
                    _light.SetActive(true);
                }

                _soundManager.PlayClick();
            }
       
        }
            CrouchUpdate();

    }

    //This handles whether we are crouching - splitting update into multiple functions makes it easier to keep track of!
    private void CrouchUpdate()
    {
       //When the player is both touching the ground and holding the left control key, we let them crouch
        
        if (_grounded)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                _isCrouching = true;
                Debug.Log("isCrouching@");
            }
            else
            {
                _isCrouching = false;
            }
        }
        else
        {
            _isCrouching = false;
        }

        //This lerps the camera slowly between standing and crouching, based on the above bool
        if(_isCrouching)
        {            
          _yCamPoint = Mathf.Lerp(_yCamPoint, _crouchingYCamPoint, _crouchTransSpeed * Time.deltaTime);
        }
        else
        {
            _yCamPoint = Mathf.Lerp(_yCamPoint, _standingYCamPoint, _crouchTransSpeed * Time.deltaTime);
        }

        _cameraParent.localPosition = new Vector3(_cameraParent.localPosition.x, _yCamPoint, _cameraParent.localPosition.z);

    }

    private void MoveBackUpdate()
    {
       Vector3 _moveDir = (_moveBackTarget - transform.position).normalized;
        _controller.Move(_moveDir * Time.deltaTime * _moveBackSpeed);
    }

    public IEnumerator MoveBack()
    {
       _moveBackTarget = _backPoint.position;
        _isMovingBack = true;
        yield return new WaitForSeconds(1);
        _isMovingBack = false;
    }

}
