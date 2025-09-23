using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanglingLight : Interactable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float _touchCoolDown;
    [SerializeField] private float _currentTouchCoolDown;
    public bool _canPull = true;

    [Header("Bulbs")]
    [SerializeField] private GameObject _bulbLit;
    [SerializeField] private GameObject _bulbUnlit;
    [SerializeField] private bool _lit = false;
    [SerializeField] private AudioSource _clickAudio;
    
    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
        if (!_canPull)
        {
            _currentTouchCoolDown -= Time.deltaTime;
            if(_currentTouchCoolDown <= 0)
            {
                _canPull = true;
            }
        }
    }

    public override void Interact()
    {
        if (_canPull)
        {
             base.Interact();
            CallAnimation();
        }
        //
    }

    private void CallAnimation()
    {

        _canPull = false;
        _currentTouchCoolDown = _touchCoolDown;
        _anim.SetTrigger("pull");
    }

    private void CallBulb()
    {
        if(_clickAudio != null)
        {
         _clickAudio.Play();
        }
        _bulbLit.SetActive(!_lit);
        _bulbUnlit.SetActive(_lit);

        _lit = !_lit;
    }
}
