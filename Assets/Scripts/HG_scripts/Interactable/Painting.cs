using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : Interactable
{


    [SerializeField] private BoxCollider _holeCollider;
    
    private void Start()
    {
       base.Start();
        _holeCollider.enabled = false;
    }

    public override void Interact()
    {
        base.Interact();
        transform.GetComponent<Animator>().SetTrigger("fall");
        

    }

    public void Shatter()
    {
        transform.GetComponent<AudioSource>().Play();
        _holeCollider.enabled = true;
    }

    private void Update()
    {
        base.Update();
    }
}
