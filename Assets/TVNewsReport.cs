using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVNewsReport : Interactable
{

    [SerializeField] private Animator _tvAnimation;
    
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
        _tvAnimation.SetTrigger("play");
    }
}
