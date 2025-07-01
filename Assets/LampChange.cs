using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampChange : MonoBehaviour
{

    public float _clickCoolDown;
    private float _currentClickCoolDown;
    public bool _canClick;
    public lampMini _lamp;
    public int _currentIteration;
    bool on = true;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_canClick)
        {
            if (Input.GetButtonDown("Interact"))
            {
                LampSwitch(true);
            }

        }
        else
        {
            _currentClickCoolDown -= Time.deltaTime;
            if(_currentClickCoolDown <= 0)
            {
                _canClick = true;
            }
        }
        
    }

    private void LampSwitch(bool turnOn)
    {
        _canClick = false;
        _currentClickCoolDown = _clickCoolDown;

        if (turnOn)
        {

        }

        _lamp.Interact();
    }
}
