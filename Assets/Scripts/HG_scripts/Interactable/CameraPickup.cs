using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickup : Interactable
{
    [SerializeField] private CameraController _camController;
    
    public override void Interact()
    {
        base.Interact();
        _camController.GotCamera();
        base.PickUpItem();
    }
}
