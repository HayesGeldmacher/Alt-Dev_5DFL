using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //SerializeField makes the variable visible in the inspector even when its private!
    [Header ("Look Variables")]
    [SerializeField] private float _mouseSensitivityX;
    [SerializeField] private float _mouseSensitivityY;
    private float _xRotation = 0f;
    private float _yRotation = 0f;
    private float _zRotation = 0f;

    private float _xMove;
    private float _yMove;

   
    [SerializeField] private Animator _camAnimator;

    [Header("Lerp Variables")]
    [SerializeField] private float _lerpZMax;
    [SerializeField] private float _lerpZSpeed;
    [SerializeField] private float _lerpYMax;
    [SerializeField] private float _lerpYSpeed;

    [Header("Interact Variables")]
    [SerializeField] private Animator _cursorAnim;
    [SerializeField] private float _interactRange;
    [SerializeField] private float _doorRange;
    private bool _isHolding = false;
    private Door _door;
    public List<string> _keys = new List<string>();
    private Interactable _currentInteractable;


    [Header("Screenshot Variables")]
    [SerializeField] private ScreenshotHandler _handler;
    [SerializeField] private SoundManager _interactAudio;
    [SerializeField] private Animator _whiteAnimator;
    [SerializeField] private AudioSource _camAudio;
    [SerializeField] private float _shotWait;
    private float _currentShotWait;

    [Header("Camera Obtained Variables")]
    public bool _hasCamera;
    [SerializeField] private GameObject _camHud;




    [Header("")]
     

    //Here, we are getting a reference to the actual player that the camera is a child of
    [SerializeField] private Transform _playerBody;

    //This decides what elements we can interact with in the game world
    [SerializeField] private LayerMask _interactable;


    void Start()
    {
        //This makes the cursor invisible and locked to screen when playing the game
        Cursor.lockState = CursorLockMode.Locked;

        //Ensuring there is no camerashot lag when starting the game
        _currentShotWait = _shotWait;

        //Makes it so the camera only animates to shift when camera is actually obtained
        if (_hasCamera)
        {
            _camAnimator.SetBool("still", false);
            _camHud.SetActive(true);

        }
        else
        {
            _camAnimator.SetBool("still", true);
            _camHud.SetActive(false);
        }
    }

    private void Update()
    {
        InteractUpdate();
        if (_isHolding)
            return;

        //Here, we are getting the actualy mouse movement from the player and converting it to variables
        //All inputs should be multiplied Time.deltaTime in order for physics to work correctly
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivityY * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        
        //The below snippet lerps the camera from side to side depending on which direction the player is walking
        float xMove = Input.GetAxis("Horizontal");
        
        if(Mathf.Abs(_xMove) > Mathf.Abs(xMove))
        {
           _zRotation = Mathf.Lerp(_zRotation, 0, _lerpZSpeed * Time.deltaTime);
        }

        else if (xMove > 0.1f)
        {
            _zRotation = Mathf.Lerp(_zRotation, -_lerpZMax, _lerpZSpeed * Time.deltaTime);

        }
        else if (xMove < -0.1f)
        {
            _zRotation = Mathf.Lerp(_zRotation, _lerpZMax, _lerpZSpeed * Time.deltaTime);
        }
        else
        {
            _zRotation = Mathf.Lerp(_zRotation, 0, _lerpZSpeed * Time.deltaTime);
        }

        _xMove = xMove;
       
       //The below snippet lerps the camera forward and backward depending on which direction the player is walking 
        float yMove = Input.GetAxis("Vertical");

        if(Mathf.Abs(_yMove) > Mathf.Abs(yMove))
        {
            _yRotation = Mathf.Lerp(_yRotation, 0, _lerpYSpeed * Time.deltaTime);
        }
        else if (yMove > 0.1f)
        {
            _yRotation = Mathf.Lerp(_yRotation, _lerpYMax, _lerpYSpeed * Time.deltaTime);
        }
        else if (yMove < -0.1f)
        {
            _yRotation = Mathf.Lerp(_yRotation, -_lerpYMax, _lerpYSpeed * Time.deltaTime);
        }
        else
        {
            _yRotation = Mathf.Lerp(_yRotation, 0, _lerpYSpeed * Time.deltaTime);
        }

        _yMove = yMove;

        //This rotates the player's body side to side when aiming with the camera
        _playerBody.Rotate(Vector3.up * mouseX);

        //This rotates the camera up and down, forward/backward, and side to side
        transform.localRotation = Quaternion.Euler(_xRotation, 0, _zRotation);

        //This rotates the camera holder up and down when the player walks forward or backward
        transform.parent.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);

        
    }

    private void InteractUpdate()
    {
       //Every frame we are shooting a raycast out into the environment. 
       //This checks if an interactable object is in front of us

        RaycastHit _hitInfo = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out _hitInfo, _interactRange, _interactable))
        {

            //here we make the cursor appear on screen!
            if (_hitInfo.transform.GetComponent<Interactable>()) 
            { 
                _cursorAnim.SetBool("isCasting", true);
                _currentInteractable = _hitInfo.transform.GetComponent<Interactable>();
                if (!_currentInteractable._isOutlined)
                {
                    _currentInteractable.OnOutline();
                }

                if (Input.GetMouseButton(0))
                {
                    if (_hitInfo.transform.tag == "door")
                    {
                        _door = _hitInfo.transform.GetComponent<Door>();
                        if (!_door._isLocked)
                        {
                        _door._isHeld = true;
                        _isHolding = true;

                        }
                        else
                        {
                            string code = _door._keyName;
                            if (_keys.Contains(code))
                            {
                                _door.Unlock();
                           
                            }
                            else
                            {
                            _door = null;

                            }
                        
                        }
                    }

                    else if (Input.GetMouseButtonDown(0))
                    {
                        _cursorAnim.SetTrigger("clicked");
                        _interactAudio.PlayInteract();
                        _hitInfo.transform.GetComponent<Interactable>().Interact();
                    }

                }
                else if(Input.GetMouseButtonUp(0))
                {
                    _isHolding = false;
                    _cursorAnim.SetBool("isCasting", false);

                    if (_door != null)
                    {
                        _door._isHeld = false;
                        _door = null;
                    }
                }
            
            }
        }
        else
        {
            if (_isHolding)
            {
                float distance = Vector3.Distance(_door.transform.position, transform.position);

                if (Input.GetMouseButtonUp(0) || distance > _doorRange)
                {
                _isHolding = false;
                _cursorAnim.SetBool("isCasting", false);
                    
                    if(_door != null)
                    {
                        _door._isHeld = false;
                        _door = null;
                    }
                }
            }
            else
            {
                _cursorAnim.SetBool("isCasting", false);
            }

            if (_currentInteractable)
            {
                _currentInteractable.StopInteract();
                _currentInteractable = null;
            }
        }

        ScreenShotUpdate();
    }

    private void ScreenShotUpdate()
    {
       //Ensures that player has to wait between each shot, cant spam
        if (Input.GetKeyDown(KeyCode.Space) && _currentShotWait >= _shotWait)
        {
            if (_hasCamera)
            {
            ScreenShot();

            }
        }

        _currentShotWait += Time.deltaTime;
    }

    private void ScreenShot()
    {
      //This lines calls to the actual screenshot object
        _handler.GetComponent<ScreenshotHandler>().TakeScreenshot_Static(Screen.width, Screen.height);
        _whiteAnimator.SetTrigger("snap");

        _camAudio.pitch = Random.Range(0.8f, 1.2f);
        _camAudio.Play();

        _currentShotWait = 0;
    }

    private void GotCamera()
    {
//Set post-processing as welL!
        _hasCamera = true;
        _camHud.SetActive(true);

    }
   
}
