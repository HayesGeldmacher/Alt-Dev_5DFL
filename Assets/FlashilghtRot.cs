using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashilghtRot : MonoBehaviour
{


    [SerializeField] private Transform _body;
    [SerializeField] private Transform _cam;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private float _speed;
    public bool _following = true;
    private bool _followingLastFrame = true;

    private void Update()
    {

        
        
        
        if (_following)
        {
                   
            FlashUpdate();
        }

        _followingLastFrame = _following;
    }

    private void FlashUpdate()
    {

        //WE KNOW THE BUG - LERPING IS GOING FROM 360 TO 1, CROSSING THE BORDER THE LONG WAY

        transform.position = _body.position;
        _targetPoint.position = _body.position;
       
        //now that rotation is figured out, we need to get this lerped!
       _targetPoint.rotation = _body.rotation;
        Vector3 _camRot = _cam.localEulerAngles;
        _targetPoint.rotation = Quaternion.Euler(_camRot.x, _targetPoint.localEulerAngles.y, _targetPoint.localEulerAngles.z);

        if (!_followingLastFrame)
        {
            transform.rotation = _targetPoint.rotation;
        }


        float _goalX = _targetPoint.localEulerAngles.x;
        float _goalY = _targetPoint.localEulerAngles.y;
        float _goalZ = _targetPoint.localEulerAngles.z;


        float _currentX = Mathf.LerpAngle(transform.localEulerAngles.x, _goalX, _speed * Time.deltaTime);
        float _currentY = Mathf.LerpAngle(transform.localEulerAngles.y, _goalY, _speed * Time.deltaTime);
        float _currentZ = Mathf.LerpAngle(transform.localEulerAngles.z, _goalZ, _speed * Time.deltaTime);


        // transform.rotation = _targetPoint.rotation;
        //transform.rotation = new Vector3(_currentX, _currentY, _currentZ);

        //float _currentX = Mathf.LerpAngle(transform.localEulerAngles.x, _goalX, _speed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(_currentX, _currentY, _currentZ);


    }
}
