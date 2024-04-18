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
    [SerializeField] private AudioSource _footSteps;
    [SerializeField] private float _neededFootTime;
    private float _currentFootTime;
    public List<AudioClip> _audioClips = new List<AudioClip>();

    [Header("Crouch Variables")]
    [SerializeField] private Transform _cameraParent;
    [SerializeField] private float _crouchSpeed = 6;
    [SerializeField] private float _crouchTransSpeed;
    [SerializeField] private float _crouchingYCamPoint = 0;
    [SerializeField] private BoxCollider _bc;
    private bool _isCrouching;
    private float _yCamPoint = 0;
    private float _standingYCamPoint = 0;
    private bool _canStand;

    [Header("Interaction Variables")]
    [SerializeField] private Transform _backPoint;
    [SerializeField] private float _moveBackSpeed;
    private bool _isMovingBack = false;
    private Vector3 _moveBackTarget;
    [SerializeField] private GameObject _light;
    [SerializeField] private SoundManager _soundManager;

    [Header("Noise Values")]
    [SerializeField] private List<MonsterRoaming> _monsters = new List<MonsterRoaming>();
    [SerializeField] private float _walkingNoise;
    [SerializeField] private float _crouchingNoise;


    [HideInInspector] public bool _frozen = false;
   
    


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

        _currentFootTime = _neededFootTime;

        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Roaming"))
        {
            _monsters.Add(monster.GetComponent<MonsterRoaming>());
        }
    }

    private void Update()
    {
        if(_frozen) return;
        
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
                //play footsteps here
                FootStepUpdate();

                if(_monsters.Count > 0)
                {
                    if (_isCrouching)
                    {
                        AddNoise(_crouchingNoise);
                    }
                    else
                    {
                        AddNoise(_walkingNoise);
                    }

                }

            }
            else
            {
                _camAnim.SetBool("walking", false);
                _currentFootTime = 0;
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
                if (_isCrouching)
                {
                    _controller.height = 2f;
                    _controller.center = new Vector3(0, 0, 0);
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
        if(_isCrouching)
        {            
          _yCamPoint = Mathf.Lerp(_yCamPoint, _crouchingYCamPoint, _crouchTransSpeed * Time.deltaTime);
          
          //Check for raycast to stand
          
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

    private void FootStepUpdate()
    {

        _currentFootTime += Time.deltaTime;
        if(_currentFootTime >= _neededFootTime)
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

    private void AddNoise(float noise)
    {
        foreach(MonsterRoaming monster in _monsters)
        {
            monster.AddNoise(noise * Time.deltaTime);
        }
    }

    public void StopCrouchInstant()
    {
        _isCrouching = false;
        _cameraParent.localPosition = new Vector3(_cameraParent.localPosition.x, _standingYCamPoint, _cameraParent.localPosition.z);
    }

}
