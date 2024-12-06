using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenSpriteFollowMouse : MonoBehaviour
{
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private Animator _anim;
    private AudioSource _clickSound;

    [Header("Boundary Constraints")]
    [SerializeField] private bool _constrained = false;
    [SerializeField] private int _minX;
    [SerializeField] private int _maxX;
    [SerializeField] private int _minY;
    [SerializeField] private int _maxY;

    [SerializeField] private Vector2 _readCursorPosition;

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

}
