using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRingTriggerNight : MonoBehaviour
{

    private bool _hasEncountered = false;
    [SerializeField] PhoneNight1 _phoneNight;


    private void OnTriggerEnter(Collider other)
    {
        if (!_hasEncountered)
        {
            if (other.gameObject.tag == "Player")
            {
                _hasEncountered = true;

                if (_phoneNight != null)
                {
                    _phoneNight.StartRinging();
                }
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
