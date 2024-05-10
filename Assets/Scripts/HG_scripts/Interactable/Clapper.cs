using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clapper : Interactable
{

    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _clapSound;

    private bool _hasPicture = false;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();
        _anim.SetTrigger("clap");
    }

}
