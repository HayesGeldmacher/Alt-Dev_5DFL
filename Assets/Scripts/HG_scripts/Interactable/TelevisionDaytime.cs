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



    public override void Interact()
    {
        if (!_hasInteracted)
        {
            _hasInteracted = true;
            _isInteracting = true;
            StartCoroutine(StartSequence());
        }
        else if (_isInteracting) 
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
        _completed = true;
    }
}
