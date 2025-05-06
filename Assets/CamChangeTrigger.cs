using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangeTrigger : MonoBehaviour
{


    [SerializeField] private CamChangePositions _camChange;
    [SerializeField] private int _camNumForward;
    [SerializeField] private int _camNumBack;
    private bool _forward = true;
    private bool _canEnter = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canEnter)
        {
            if(other.tag == "Player")
            {
                if (_forward) 
                {
                    _camChange.ChangePos(_camNumForward);
                }
                else
                {
                    _camChange.ChangePos(_camNumBack);
                }

                _forward = !_forward;
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
