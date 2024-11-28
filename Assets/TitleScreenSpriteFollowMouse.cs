using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenSpriteFollowMouse : MonoBehaviour
{
    [SerializeField] private Canvas _parentCanvas;
    private Animator _anim;
    private AudioSource _clickSound;

    // Start is called before the first frame update
    void Start()
    {
        _anim = transform.GetComponent<Animator>();
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

        transform.position = _parentCanvas.transform.TransformPoint(_pos);
    }
}
