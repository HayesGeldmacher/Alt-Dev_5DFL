using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashilghtRot : MonoBehaviour
{


    [SerializeField] private Transform _body;
    [SerializeField] private Transform _cam; 

    private void Update()
    {
        FlashUpdate();
    }

    private void FlashUpdate()
    {
        transform.position = _body.position;
        transform.rotation = _body.rotation;
        Vector3 _camRot = _cam.localEulerAngles;
        Vector3 bodyRot = _body.localEulerAngles;
        transform.rotation = Quaternion.Euler(_camRot.x, transform.localEulerAngles.y, transform.localEulerAngles.z);



        
        //transform.rotation = Quaternion.Euler(_cam.rotation.x, _body.rotation.y, _body.rotation.z);

        Debug.Log("FUCK!");
    }
}
