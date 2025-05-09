using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3rdPerson : MonoBehaviour
{
    //the below variables check wether the player is touching the ground or not!
    [Header("Grounded Variables")]
    [SerializeField] private bool _grounded;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _gravity;

    [Header("Walking Variables")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private AudioSource _footSteps;
    [SerializeField] private float _neededFootTime;
    [SerializeField] private float _neededFootRunTime;

    private float _currentFootTime;
    public List<AudioClip> _audioClips = new List<AudioClip>();
    private float _walkVolume;
    [SerializeField] private float _runVolume;


    [Header("Body Variables")]
    public bool _animateBody = false;
    [SerializeField] private Animator _bodyAnim;
    [HideInInspector] public float _moveMag;


    [Header("Running")]
    [SerializeField] private float _runSpeed;
    [SerializeField] private bool _running;
    [SerializeField] private AudioSource _breathing;
    [SerializeField] private AudioClip[] _breathingClips;
    [SerializeField] private Animator _runAnim;

    [Header("Crouch Variables")]
    [SerializeField] private Transform _cameraParent;
    [SerializeField] private float _crouchSpeed = 6;
    [SerializeField] private float _crouchTransSpeed;
    [SerializeField] private float _crouchingYCamPoint = 0;
    [SerializeField] private BoxCollider _bc;
    [SerializeField] private LayerMask _standMask;
    [SerializeField] private float _crouchRange;
    [SerializeField] private bool _isCrouching;
    private float _yCamPoint = 0;
    private float _standingYCamPoint = 0;
    private bool _canStand = true;

    [Header("turn Sensitivity")]
    [SerializeField] private Transform _playerBody;
    [SerializeField] private float _turnSensitivity;

    [Header("Interactable")]
    [SerializeField] private float _interactRange;
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private Animator _clickAnim;
    [SerializeField] private Transform _castPos;
    public bool _frozen = false;


    //The below region just creates a reference of this specific controller that we can call from other scripts quickly
    #region Singleton

    public static PlayerController3rdPerson instance;

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

        _currentFootTime = _neededFootTime;

        _walkVolume = _footSteps.volume;
        _running = false;

    }

    private void Update()
    {


        if (_frozen) return;
        
        CrouchUpdate();
        RaycastUpdate();

        //This line checks if the player is touching the ground, or in the air
        //_grounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        // Does the ray intersect any objects excluding the player layer
        RaycastHit groundhit;
            if (Physics.Raycast(_groundCheck.position, -transform.up, out groundhit, _groundDistance, _groundMask))
            {
                _grounded = true;
            }
            else
            {
                _grounded = false;
            }


            //takes the raw player input to move character 
            float x = Input.GetAxis("Horizontal") * _turnSensitivity;
            float z = Input.GetAxis("Vertical");

             _playerBody.Rotate(Vector3.up * x);

             //Stores that input in a variable to be used later in function
             Vector3 _move = (transform.forward * z);
            _moveMag = _move.magnitude;

        if (_moveMag <= 0.1f)
        {
            if(Mathf.Abs(x) > 0.1f)
            {
                _bodyAnim.SetBool("turning", true);
            }
            else
            {
                _bodyAnim.SetBool("turning", false);
            }
        }
        else
        {
            _bodyAnim.SetBool("turning", false);
        }

        //Constantly adding a downward force to the player so they fall when not standing on something

        if (_grounded)
        {
            _velocity.y = -10;
        }
        else
        {
            if (Mathf.Abs(_velocity.y) < 10)
            {
                _velocity.y += _gravity * Time.deltaTime;
            }
        }


            //Checking to see if the player is running!
            if (_grounded && Input.GetButton("run") && !_isCrouching)
            {
                if (_move.magnitude > 0.1f)
                {
                    _running = true;

                    if (_runAnim != null)
                    {
                        _runAnim.SetBool("running", true);
                    }

                }
                else
                {
                    _running = false;

                    if (_runAnim != null)
                    {
                        _runAnim.SetBool("running", false);
                    }
                }
            }
            else
            {
                _running = false;
                _breathing.loop = false;

                if (_runAnim != null)
                {
                    _runAnim.SetBool("running", false);
                }
            }

            float _speed;
            if (_running)
            {
                _speed = _runSpeed;
            }
            else if (_isCrouching)
            {
                _speed = _crouchSpeed;
            }
            else
            {
                _speed = _walkSpeed;
            }

            //Setting run animations
            if (_animateBody)
            {
                if (_running)
                {
                    if (_bodyAnim != null)
                    {
                        _bodyAnim.SetBool("running", true);
                    }
                }
                else
                {
                    if (_bodyAnim != null)
                    {
                        _bodyAnim.SetBool("running", false);
                    }
                }
            }

            //controller.move is how the character actually moves - always multiply by Time.deltaTime so physics work correctly!
            _controller.Move(_move * _speed * Time.deltaTime);
            _controller.Move(_velocity * Time.deltaTime);

            //Controls whether the camera is animating to walk or not
            if (_move.magnitude > 0.1f)
            {



                if (_animateBody)
                {
                    if (_bodyAnim != null)
                    {
                        _bodyAnim.SetBool("walking", true);
                    }
                }

                //play footsteps here
                FootStepUpdate();
            }
            else
            {


                if (_animateBody)
                {
                    if (_bodyAnim != null)
                    {
                        _bodyAnim.SetBool("walking", false);
                    }
                }

                _currentFootTime = 0;
            }


      
    }

    //This handles whether we are crouching - splitting update into multiple functions makes it easier to keep track of!
    private void CrouchUpdate()
    {
        //When the player is both touching the ground and holding the left control key, we let them crouch

        if (_grounded)
        {
            if (!_running)
            {
                if (Input.GetButton("crouch"))
                {
                    if (!_isCrouching)
                    {
                        _controller.height = 1.4f;
                        _controller.center = new Vector3(0, -0.4f, 0);
                        _isCrouching = true;
                        Debug.Log("isCrouching@");

                    }
                }
                else
                {
                    if (_isCrouching && _canStand)
                    {
                        _controller.height = 2.3f;
                        _controller.center = new Vector3(0, 0.1f, 0);
                        _isCrouching = false;
                    }
                }
            }
            else
            {
                if (_isCrouching)
                {
                    _isCrouching = false;
                }
            }

            //This lerps the camera slowly between standing and crouching, based on the above bool
            if (_isCrouching)
            {
               

                if (_animateBody)
                {
                    if (_bodyAnim != null)
                    {
                        _bodyAnim.SetBool("crouching", true);
                    }
                }
            }
            else
            {
             

                if (_animateBody)
                {
                    if (_bodyAnim != null)
                    {
                        _bodyAnim.SetBool("crouching", false);
                    }
                }
            }


        }

    }



    private void FootStepUpdate()
    {

        _currentFootTime += Time.deltaTime;

        if (_running)
        {


            if (!_breathing.isPlaying)
            {

                _breathing.pitch = Random.Range(0.95f, 1.05f);
                _breathing.Play();

            }
            _footSteps.volume = _runVolume;
            if (_currentFootTime >= _neededFootRunTime)
            {
                if (!_footSteps.isPlaying)
                {
                    int _randomAudio = Random.Range(0, _audioClips.Count);
                    _footSteps.clip = _audioClips[_randomAudio];
                    _footSteps.Play();
                    _currentFootTime = 0;
                }

            }

        }
        else
        {
            _footSteps.volume = _walkVolume;

            if (_currentFootTime >= _neededFootTime)
            {
                if (!_footSteps.isPlaying)
                {
                    int _randomAudio = Random.Range(0, _audioClips.Count);
                    _footSteps.clip = _audioClips[_randomAudio];
                    _footSteps.Play();
                    _currentFootTime = 0;
                }

            }
        }
    }



    private IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(0.2f);
        _running = false;
    }


    private void RaycastUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(_castPos.position, transform.forward, out hit, _interactRange, _interactMask))
        {
            if (Input.GetButtonDown("Interact"))
            {
                hit.transform.GetComponent<Interactable>().Interact();
                _clickAnim.SetTrigger("click");
            }

            _clickAnim.SetBool("casting", true);
            Debug.Log("Casting!!");
        }
        else
        {
            _clickAnim.SetBool("casting", false);
        }
        
        
    }

}
