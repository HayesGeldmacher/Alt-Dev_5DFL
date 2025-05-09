using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monitor3rdPerson : Interactable
{
    [SerializeField] private CutsceneManager _scene;
    private bool _started = false;
    [SerializeField] private PlayerController3rdPerson _player3rd;
    private void Start()
    {

    }

    private void Update()
    {
        base.Update();
    }

    public override void Interact()
    {

        if (!_started)
        {
            _player3rd._frozen = true;
            _started = true;
            base.Interact();
            _scene.Begin();

        }
    }
}
