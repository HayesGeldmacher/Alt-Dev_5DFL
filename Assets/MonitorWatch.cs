using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorWatch : Interactable
{

    public bool _activated = false;
    [SerializeField] private Animator _anim;

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
         base.Interact();
         _activated = true;
        }
    }
}
