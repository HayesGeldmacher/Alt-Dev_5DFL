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
}
