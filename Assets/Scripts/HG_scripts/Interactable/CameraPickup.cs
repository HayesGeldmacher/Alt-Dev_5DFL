using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickup : Interactable
{
    [SerializeField] private CameraController _camController;
    [SerializeField] private GameObject _balloon;
    private bool _started = false;

    [SerializeField] private MeshRenderer _render;
    [SerializeField] private BoxCollider _bc;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _interactAudio;
    [SerializeField] private int _lines;
    [SerializeField] private PlayerController _controller;

    private void Start()
    {
        base.Start();
        _render = GetComponent<MeshRenderer>();
        _bc = GetComponent<BoxCollider>();

    }
    private void Update()
    {
       // base.Update();

        if (Input.GetMouseButtonDown(0) && _started)
        {
            if(_lines <= 0)
            {
                base.EndDialogue();
                Debug.Log("Camover!");
                _controller._frozen = false;
                Destroy(gameObject);
            }
            else
            {
            _lines -= 1;
            base.Interact();
            _interactAudio.Play();

            }
        }
    }

    public override void Interact()
    {
        if (!_started)
        {
            _controller._frozen = true;
            _lines -= 1;
            _started = true;
            base.Interact();
             _camController.GotCamera();
            _bc.enabled = false;
            _render.enabled = false;
            StartCoroutine(NextInteract());
        }
    }

    private IEnumerator NextInteract()
    {
        yield return new WaitForSeconds(2);
        _audio.Play();
    }
}
