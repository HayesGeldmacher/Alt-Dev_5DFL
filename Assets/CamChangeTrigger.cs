using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangeTrigger : MonoBehaviour
{


    [SerializeField] private CamChangePositions _camChange;
    [SerializeField] private int _camNum;
    private bool _forward = true;
    private bool _canEnter = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canEnter && _camNum != _camChange._currentCam)
        {
            if(other.tag == "Player")
            {
               _camChange.ChangePos(_camNum); 
               _canEnter = false;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _canEnter = true;
        }
    }

}
