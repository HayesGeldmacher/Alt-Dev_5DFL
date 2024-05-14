using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Interactable
{

    [SerializeField] private CameraController _cam;
    [SerializeField] private Animator _letterAnim;
    [SerializeField] private int _timesInteracted = 0;
    [SerializeField] private AudioSource _interactAudio;
    [SerializeField] private PlayerController _controller;
    private bool _hasInteracted = false;
    private bool _startedInteracting = false;
    

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
        if (_hasInteracted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_timesInteracted < 4)
                {
                base.Interact();
                _timesInteracted++;
                _interactAudio.Play();

                }
                else
                {
                   StartCoroutine(EndInteract());
                }
            }

        }
    }

    public override void Interact()
    {
        if (_hasInteracted)
        {
          
        }
        else
        {
            if (!_startedInteracting)
            {
                _startedInteracting = true;
                _cam._canInteract = false;
                _letterAnim.SetTrigger("open");
                _controller._frozen = true;
                StartCoroutine(StartInteract());

            }

        }
    }

    private IEnumerator StartInteract()
    {
        yield return new WaitForSeconds(1f);
        _startedInteracting = false;
        _hasInteracted = true;
        base.Interact();
        _timesInteracted++;
    }

    private IEnumerator EndInteract()
    {
        base.EndDialogue();
        _hasInteracted = false;
        _startedInteracting = false;
        _timesInteracted = 0;
        _letterAnim.SetTrigger("close");
        _controller._frozen = false;
        yield return new WaitForSeconds(1f);
        _cam._canInteract = true;
    }
}

