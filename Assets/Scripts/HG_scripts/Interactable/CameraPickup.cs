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
    [SerializeField] private GameObject _balloon;
    [SerializeField] private Animator _balloonParent;
    [SerializeField] private bool _intro = false;
    [SerializeField] private CameraController _cam;
    [SerializeField] private GameObject _glimmer;

    [SerializeField] private GameObject _ghostCam;


    private void Start()
    {
        base.Start();
        _balloon.SetActive(false);
        _render = GetComponent<MeshRenderer>();
        _bc = GetComponent<BoxCollider>();
        _ghostCam.SetActive(false);

    }
    private void Update()
    {
        if (!_intro) return;

        if (Input.GetMouseButtonDown(0) && _started)
        {
            if(_lines <= 0)
            {
                EndCamera();
            }
            else
            {
           
                if(_lines == 2)
                {
                    _balloon.SetActive(true);
                    _balloonParent.SetTrigger("float");
                }
            
                _lines -= 1;
                base.Interact();
                _interactAudio.Play();

            }
        }
    }

    public override void Interact()
    {
        if (_intro)
        {
            if (!_started)
            {
                _cam._canInteract = false;
                _controller._frozen = true;
                Destroy(_glimmer);
                _lines -= 1;
                _started = true;
                base.Interact();
                 _camController.GotCamera();
                _bc.enabled = false;
                _render.enabled = false;
                _ghostCam.SetActive(true);
                StartCoroutine(NextInteract());
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
}
