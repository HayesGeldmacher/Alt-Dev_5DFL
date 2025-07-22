using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Security.Cryptography;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.LowLevel;

public class TitleScreenSpriteFollowMouse : MonoBehaviour
{

    [SerializeField] private Transform _mouseVisual;
    [SerializeField] private RectTransform _canvasTransform;
    [SerializeField] private Transform _virtualMouseParent;
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private Animator _anim;
    private AudioSource _clickSound;
    [SerializeField] private RawImage _clickImage;

    [SerializeField] private Vector2 _readCursorPosition;

    [Header("Boundary Constraints")]
    [SerializeField] private bool _constrained = false;
    [SerializeField] private float[] _minX;
    [SerializeField] private float[] _maxX;
    [SerializeField] private float[] _minY;
    [SerializeField] private float[] _maxY;

    [Header("Controller Boundary")]
    [SerializeField] private bool _controllerConstrained = false;
    [SerializeField] private float[] _minXController;
    [SerializeField] private float[] _maxXController;
    [SerializeField] private float[] _minYController;
    [SerializeField] private float[] _maxYController;


    [SerializeField] private bool _active = false;

    [SerializeField] private bool _controller = false;

    //Set to false when cursor should not be usable!
    public bool _controllerCanPress = false;

    [SerializeField] private VirtualMouseInput _virtualMouse;
    
    [SerializeField] private bool _hasButton = false;
    [SerializeField] private int _currentRestraint = 0;
    public bool _usingMouse;
    public bool _inTextGame = false;
    [SerializeField] private Vector2 _currentVirtPos;

  
    
    // Start is called before the first frame update
    void Start()
    {
        //_anim = transform.GetComponent<Animator>();
        _clickSound = transform.GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Confined;
        GameInputManager.OnGameDeviceChanged += OnDeviceChanged;

        _currentVirtPos = _virtualMouse.virtualMouse.position.value;


        
    }

    // Update is called once per frame
    void Update()
    {

        if (_inTextGame)
        {
            _currentRestraint = 1;
        }
        else
        {
            _currentRestraint = 0;
        }


        _virtualMouseParent.localScale = Vector3.one * (1f / _canvasTransform.localScale.x);
        _mouseVisual.localScale = Vector3.one * (_canvasTransform.localScale.y);
        _virtualMouseParent.SetAsLastSibling();
        

        if (_usingMouse && _active)
        {
            MouseUpdate();
        }

        ControllerUpdate();
        
       if(!_active) return;

        if(Input.GetButtonDown("Interact"))
        {
            _anim.SetTrigger("click");
            _clickSound.Play();
        }


        
    }

    private void MouseUpdate()
    {
        
        Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _cursorPos;

        Vector2 _pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_parentCanvas.transform, Input.mousePosition, _parentCanvas.worldCamera, out _pos);

        if (_constrained)
        {
            float xPos = Mathf.Clamp(_pos.x, _minX[_currentRestraint], _maxX[_currentRestraint]);
            float yPos = Mathf.Clamp(_pos.y, _minY[_currentRestraint], _maxY[_currentRestraint]);
            _pos = new Vector2(xPos, yPos);
        }

        transform.position = _parentCanvas.transform.TransformPoint(_pos);
        _readCursorPosition = _pos;
    }

    private void ControllerUpdate()
    {
      
            if (!_active)
            {
                InputState.Change(_virtualMouse.virtualMouse.position, _currentVirtPos);
            }
            else if(_controllerConstrained)
            {
                Vector2 newVirtPos = _virtualMouse.virtualMouse.position.value;
                //Adding borders


                newVirtPos.x = Mathf.Clamp(newVirtPos.x, _minXController[_currentRestraint], _maxXController[_currentRestraint]);
                newVirtPos.y = Mathf.Clamp(newVirtPos.y, _minYController[_currentRestraint], _maxYController[_currentRestraint]);

                InputState.Change(_virtualMouse.virtualMouse.position, newVirtPos);

            }
            else
            {
                Vector2 newVirtPos = _virtualMouse.virtualMouse.position.value;

                //Adding borders
                float borderX = Screen.width / 10;
                float borderYMax = Screen.height / 10;
                float borderYMin = Screen.height / 7;

                newVirtPos.x = Mathf.Clamp(newVirtPos.x, 0f + borderX, Screen.width - borderX);
                newVirtPos.y = Mathf.Clamp(newVirtPos.y, 0f + borderYMin, Screen.height - borderYMax);

                InputState.Change(_virtualMouse.virtualMouse.position, newVirtPos);

            }
    }


    public void EnableCursor(bool _kill)
    {
        Debug.Log("ENABLED : " + _kill);

        _active = _kill;

        if (!_kill)
        {
            if(_virtualMouse != null)
            {
               //problem LINE!!! - theres gotta be something not instantiated here
                _currentVirtPos = _virtualMouse.virtualMouse.position.value;
            }
            _clickImage.enabled = false;
        }
        else
        {
           StartCoroutine(EnableSprite());
        }
    }

    private IEnumerator EnableSprite()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _clickImage.enabled = true;
    }

    private void OnDeviceChanged()
    {


        //This is where we get rid of the virtual mouse!
        Debug.Log("DEVICE CHANGED BITCH WHOOO");

        _usingMouse = GameInputManager.instance._usingMouse;

    }

    public void EnterTextGame(bool enter)
    {
        _inTextGame = enter;
    }

    void OnDestroy()
    {
        GameInputManager.OnGameDeviceChanged -= OnDeviceChanged;
    }
}
