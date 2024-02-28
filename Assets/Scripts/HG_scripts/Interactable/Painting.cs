using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : Interactable
{
    public override void Interact()
    {
        base.Interact();
        transform.GetComponent<Animator>().SetTrigger("fall");

    }

    public void Shatter()
    {
        transform.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        base.Update();
    }
}
