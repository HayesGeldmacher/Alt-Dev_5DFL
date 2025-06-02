using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monitor3rdPerson : Interactable
{
    private bool _canEnter = true;
    [SerializeField] private CutsceneManager _scene;
    private bool _started = false;
    [SerializeField] private PlayerController3rdPerson _player3rd;
    [SerializeField] private SingleMosh _mosh;

    private void Update()
    {
        base.Update();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (_canEnter)
        {
            if (other.tag == "Player")
            {
                Interact();
            }
        }

    }

    public override void Interact()
    {

        if (!_started)
        {
            _mosh.CallGlitch();
            _player3rd._frozen = true;
            _started = true;
            base.Interact();
            _scene.Begin();

        }
    }
}
