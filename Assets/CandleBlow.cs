using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleBlow : MonoBehaviour
{

    public float clickCoolDown;
    public float _totalCoolDown;
    public bool _canClick = false;
    public bool _tooSleepy = true;

    public Animator[] _fireAnims;

    public int _blowCount = 0;
    public int _totalBlow;

    public float[] candleBlowTimes;

    public bool _candleOut = false;

    public Animator _puppet;
    
    
    [Header("Dialogue Fields")]
    public Interactable _dialogue;
    public bool _started = false;
    public int _currentDialogue = 0;
    public int _totalDialogue = 2;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

 
    
    // Update is called once per frame
    void Update()
    {

        if (!_started)
        {
            if (Input.GetButtonDown("Interact"))
            {
                _dialogue.Interact();
                _currentDialogue++;

                if(_currentDialogue >= _totalDialogue)
                {
                    _started = true;
                }
            }
        }
        else
        {
            BlowUpdate();
        }
            
        
        
    }

    private void BlowUpdate()
    {
            if (_canClick)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (!_tooSleepy)
                    {
                        BlowCandle();
                    }    
                }
            }
            else
            {
                if (!_candleOut)
                {
                        clickCoolDown -= Time.deltaTime;
                        if(clickCoolDown <= 0)
                        {
                            _canClick = true;
                        }
                }    
            }

    }

    private void BlowCandle()
    {
        _dialogue.Interact();
        clickCoolDown = _totalCoolDown;
        _canClick = false;
        
        //make the CRT breathe shit happpen!

        if (_blowCount < _totalBlow)
        {
            StartCoroutine(BringBack(candleBlowTimes[_blowCount]));
            _blowCount++;
        }
        else
        {
            Darkness();
        }

    }

    private IEnumerator BringBack(float wait)
    {
        _candleOut = true;



        yield return new WaitForSeconds(0.5f);
        foreach (Animator flame in _fireAnims)
        {
            flame.SetTrigger("fade");
        }


        yield return new WaitForSeconds(wait);
        _puppet.SetInteger("react", _blowCount);
        _puppet.SetTrigger("trigger");

        foreach (Animator flame in _fireAnims)
        {
            flame.SetTrigger("back");
        }
        _candleOut = false;
    }
         
    private void Darkness()
    {
        _tooSleepy = true;
        foreach(Animator flame in _fireAnims)
        {
            flame.SetTrigger("out");
        }
    }
}
