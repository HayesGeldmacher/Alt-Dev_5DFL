using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TitleScreenSpriteFollowMouse : MonoBehaviour
{
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private Animator _anim;
    private AudioSource _clickSound;
    [SerializeField] private RawImage _clickImage;

    [Header("Boundary Constraints")]
    [SerializeField] private bool _constrained = false;
    [SerializeField] private int _minX;
    [SerializeField] private int _maxX;
    [SerializeField] private int _minY;
    [SerializeField] private int _maxY;

    [SerializeField] private Vector2 _readCursorPosition;
    private bool _active = false;

    private bool _controller = true;

    // Start is called before the first frame update
    void Start()
    {
        //_anim = transform.GetComponent<Animator>();
        _clickSound = transform.GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Confined;

        
    }

    // Update is called once per frame
    void Update()
    {
       
       if(!_active) return;

        if (_controller)
        {
            ControllerFollowUpdate();
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            _anim.SetTrigger("click");
            _clickSound.Play();
        }


        Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _cursorPos;

        Vector2 _pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_parentCanvas.transform, Input.mousePosition, _parentCanvas.worldCamera, out _pos);

        if (_constrained)
        {
            float xPos = Mathf.Clamp(_pos.x, _minX, _maxX);
            float yPos = Mathf.Clamp(_pos.y, _minY, _maxY);
            _pos = new Vector2(xPos, yPos);
        }

        transform.position = _parentCanvas.transform.TransformPoint(_pos);
        _readCursorPosition = _pos;
    }

    public void EnableCursor(bool _kill)
    {

        //Set position BEFORE we re-enable image to avoid jumpy glitch
        Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _cursorPos;

        Vector2 _pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_parentCanvas.transform, Input.mousePosition, _parentCanvas.worldCamera, out _pos);

        if (_constrained)
        {
            float xPos = Mathf.Clamp(_pos.x, _minX, _maxX);
            float yPos = Mathf.Clamp(_pos.y, _minY, _maxY);
            _pos = new Vector2(xPos, yPos);
        }

        transform.position = _parentCanvas.transform.TransformPoint(_pos);
        _readCursorPosition = _pos;

        _active = _kill;
        _clickImage.enabled = _kill;

    }


    private void ControllerFollowUpdate()
    {
        float mouseX = Input.GetAxis("ControllerX") * 300;
        float mouseY = Input.GetAxis("ControllerY") * 300;

        Vector2 _controllerInput = new Vector2(mouseX, mouseY);

        Vector2 _currentPos = Input.mousePosition;
        _currentPos += _controllerInput;
        

        Mouse.current.WarpCursorPosition(_currentPos);
    }

}
