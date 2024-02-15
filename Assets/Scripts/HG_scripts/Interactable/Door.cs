using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{

    public bool _isHeld = false;
    public bool _isLocked = false;
    [SerializeField] private float _doorSensitivity = 5;
    [SerializeField] private float _swingSmoothness = 5;
    [SerializeField] private Transform _doorPivot;


    
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
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
        float _newRotY  = Mathf.Clamp(newRotation.y, -110, 110);
        newRotation.y = _newRotY;
        _doorPivot.localRotation = newRotation;
       //_doorPivot.localRotation = Quaternion.Slerp(_doorPivot.localRotation, newRotation, Time.deltaTime * _swingSmoothness);
       
    }
}
