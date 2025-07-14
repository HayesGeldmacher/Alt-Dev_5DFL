using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleBlow : MonoBehaviour
{

    public float clickCoolDown;
    public float _totalCoolDown;
    public bool _canClick = false;
    public bool _tooSleepy = false;

    public Animator[] _fireAnims;
    public int _blowCount = 0;
    public int _totalBlow;
    
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
                    BlowCandle();
                }
            }
            else
            {
                clickCoolDown -= Time.deltaTime;
                if(clickCoolDown <= 0)
                {
                    _canClick = true;
                }
            }
        
    }

    private void BlowCandle()
    {
        clickCoolDown = _totalCoolDown;
        _canClick = false;
        
        //make the CRT breathe shit happpen!
        _blowCount++;
        if(_blowCount >= _totalBlow)
        {
            Darkness();
        }
        else
        {
          foreach(Animator flame in _fireAnims)
            {
                flame.SetTrigger("fade");
            }
        }
    }

    private void Darkness()
    {
        
        foreach(Animator flame in _fireAnims)
        {
            flame.SetTrigger("out");
        }
    }
}
