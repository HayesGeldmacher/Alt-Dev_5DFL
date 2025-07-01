using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LampChange : MonoBehaviour
{

    public float _clickCoolDown;
    private float _currentClickCoolDown;
    public bool _canClick;
    public lampMini _lamp;
    bool on = true;

    public int _currentIteration;
    public LampIteration[] _iterations;
    public AudioSource _breathAudio;
    public LampIteration _currentLamp;
    public AudioSource _lampClick;
    public Animator _lampAnim;

    public int _maxIterations = 8;
    public bool _tooSleepy = false;

    [Header("End Fields")]
    public CRT _crt;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!_tooSleepy)
        {
            if (_canClick)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    StartCoroutine(LampSwitch(true));
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
        
    }

    private IEnumerator LampSwitch(bool turnOn)
    {
        _lampAnim.SetTrigger("pull");
        yield return new WaitForSeconds(0.25f);
        _lampClick.Play();

        if (!on)
        {
            _lamp.Interact();
        }

        _canClick = false;
        _currentClickCoolDown = _clickCoolDown;

        if(_currentLamp != null)
        {
            _currentLamp.Activate(false);

        }

        _currentLamp = _iterations[_currentIteration];
        _currentLamp.Activate(true);
        

        if (on)
        {
            _lamp.Interact();
        }  

        _currentIteration++;
        on = !on;

        if(_currentIteration >= _maxIterations)
        {
            StartCoroutine(EndSequence());
        }
    }

    private IEnumerator EndSequence()
    {
        _tooSleepy = true;
        _crt.StartBreathing();
        yield return new WaitForSeconds(1f);
        
        Debug.Log("Sequence Ended!");
    }
}
