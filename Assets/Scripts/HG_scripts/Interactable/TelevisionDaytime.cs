using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelevisionDaytime : Interactable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private bool _isInteracting;
    [SerializeField] private bool _hasInteracted;
    [SerializeField] private bool _canMoveOn = false;
    [SerializeField] private bool _canExit = false;
    [SerializeField] private bool _completed = true;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private CameraController _camController;
    [SerializeField] Interactable _tv;
    [SerializeField] private AudioSource _staticIntro;
    [SerializeField] private AudioSource _staticContinue;
    [SerializeField] private AudioSource _turnOff;

    public override void Interact()
    {
      
        
        if (!_hasInteracted)
        {
            _staticIntro.Play();
            _hasInteracted = true;
            _isInteracting = true;
            _controller._frozen = true;
            _camController._frozen = true;
            StartCoroutine(StartSequence());
        }
        
    }

   private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _hasInteracted && !_completed)
        {
          
            if (_isInteracting)
            {
                if (_canExit)
                {
                    StartCoroutine(Disappear());
                }
                else if (_canMoveOn)
                {
                    _canMoveOn = false;
                    StartCoroutine(FaceAppear());
                }

            }
        }

        if (!_staticIntro.isPlaying && _hasInteracted)
        {
            if (!_staticContinue.isPlaying)
            {
                _staticContinue.Play();
            }
        }
    }


    private IEnumerator StartSequence()
    {
        yield return new WaitForSeconds(0.2f);
        _anim.SetTrigger("appear");
        yield return new WaitForSeconds(1f);
        _canMoveOn = true;
    }

    private IEnumerator FaceAppear()
    {
        base.Interact();
        yield return new WaitForSeconds(0.3f);
        _anim.SetTrigger("faceappear");
        yield return new WaitForSeconds(1f);
        _canExit = true;

    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.1f);
        _anim.SetTrigger("disappear");
        _turnOff.Play();
        yield return new WaitForSeconds(1);
        _controller._frozen = false;
        _camController._frozen = false;
        _completed = true;
        _tv.enabled = true;
        _staticContinue.Stop();
        _staticIntro.Stop();
        TelevisionDaytime _this = this;
        base.EndDialogue();
        Destroy(_this);
    }
}