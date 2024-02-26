using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{

    public bool _isHeld = false;
    public bool _isLocked = false;
    public Key _key;
    [HideInInspector] public string _keyName;
    [SerializeField] private float _doorSensitivity = 5;
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private float _minYRot = -0.9f;
    [SerializeField] private float _maxYRot = 0.9f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (_key)
        {
        _keyName = _key.gameObject.name.ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHeld)
        {
            HoldUpdate();   
        }
    }

    private void HoldUpdate()
    {
        float _mouseY = Input.GetAxis("Mouse Y") * _doorSensitivity;
        _mouseY *= -1;

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

    public void Unlock()
    {
        _isLocked = false;
        //play a sound!
    }
}
