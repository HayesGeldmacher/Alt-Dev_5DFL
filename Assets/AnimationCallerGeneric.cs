using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationCallerGeneric : Interactable
{

    [SerializeField] private Animator _anim;
    [SerializeField] private string _animTrigger;
    [SerializeField] private AudioSource _audio;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

    }

    public override void Interact()
    {
        base.Interact();

        if (_audio != null)
        {
            _audio.Play(); 
        }

        if(_anim != null)
        {
            _anim.SetTrigger(_animTrigger);
        } 
    }
}
