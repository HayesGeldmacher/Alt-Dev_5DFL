using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chairSit : Interactable
{

    [SerializeField] private transform _camera;

    public override void Interact()
    {
        base.Interact();
        PlayerController.instance._frozen = true;

    }
}
