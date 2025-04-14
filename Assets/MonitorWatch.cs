using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MonitorWatch : Interactable
{

    public bool _activated = false;
    [SerializeField] private Animator _anim;
    [SerializeField] private VideoPlayer _vid;


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

        if (!_activated)
        {
            _activated = true;
            _vid.Play();
            _anim.SetTrigger("play");
        }
        base.Interact();
    }
}
