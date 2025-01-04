using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickup : Interactable
{
    [SerializeField] private CameraController _camController;
    private bool _started = false;

    [SerializeField] private MeshRenderer _render;
    [SerializeField] private BoxCollider _bc;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _interactAudio;
    [SerializeField] private int _lines;
    [SerializeField] private PlayerController _controller;
    [SerializeField] private bool _intro = false;
    [SerializeField] private CameraController _cam;
    [SerializeField] private GameObject _glimmer;
    private bool _canClickContinue = false;
    [SerializeField] private GameObject _screenVHS;
    [SerializeField] private GameObject _ghostCam;


    private void Start()
    {
        base.Start();
        //_render = GetComponent<MeshRenderer>();
        _bc = GetComponent<BoxCollider>();
        _canClickContinue = false;

    }
    private void Update()
    {
        if (!_intro) return;
        if (!_canClickContinue) return;

        if (Input.GetButtonDown("Interact") && _started)
        {
            if(_lines <= 0)
            {
                EndCamera();
            }
            else
            {
           
                if(_lines == 3)
                {
                    StartCoroutine(NextInteract());
                }
            
                _lines -= 1;
                Debug.Log("Happened Once");
               base.Interact();
                _interactAudio.Play();

            }
        }
    }

    public override void Interact()
    {

        Debug.Log("interacting!");
        Destroy(_screenVHS);
        
        if (_intro)
        {
            if (!_started)
            {
                 _camController.GotCamera();
                _controller._frozen = true;
                Destroy(_glimmer);
                _lines -= 1;
                _started = true;
                base.Interact();
                _bc.enabled = false;
                _render.enabled = false;
                _ghostCam.SetActive(true);
                
            }

        }
        else
        {
            _started = true;
            base.Interact();
            _camController.GotCamera();
            _bc.enabled = false;
            _render.enabled = false;
           // StartCoroutine(NextInteract());
        }

        if (!_canClickContinue)
        {
            StartCoroutine(ClickWait());
        }
    }

    private IEnumerator NextInteract()
    {
        yield return new WaitForSeconds(2);
        _audio.Play();
    }

    private void EndCamera()
    {
        base.EndDialogue();
        Debug.Log("Camover!");
        _controller._frozen = false;
        _cam._canInteract = true;
        Destroy(gameObject);
    }
    private IEnumerator ClickWait()
    {
        yield return new WaitForSeconds(0.4f);
        _canClickContinue = true;

    }
}
