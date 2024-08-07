using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoors : Interactable
{

    [SerializeField] private bool _open = false;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minX;
    [SerializeField] private float _doorSensitivity; 
    private float _posX;
    [SerializeField] private float _goalPos;

    // Start is called before the first frame update
    void Start()
    {
        _isMoving = false;
        _posX = transform.localPosition.x;
        Debug.Log("POS:" + _posX);
    }


    public override void Interact(){

        _isMoving = true;
        
        if (_open)
        {
            _open = false;
            _goalPos = 0;
        }
        else
        {
            _goalPos = _maxX;
            _open = true;
        }
    
    }

    private void Update()
    {

    


        if( _isMoving )
        {
            float currentDist = Mathf.Abs(_goalPos - _posX);
            if (currentDist <= 0.1f)
            {
                _isMoving = false;
               Debug.Log("stopped!");
                Debug.Log(_open);
            }
        }   
            _posX = Mathf.Lerp(_posX, _goalPos, _doorSensitivity * Time.deltaTime);      
            transform.localPosition = new Vector3(_posX, transform.localPosition.y, transform.localPosition.z);
    }
}
