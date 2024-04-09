using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPickup : Interactable
{
    [SerializeField] private CameraController _camController;
    [SerializeField] private GameObject _balloon;

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
        _camController.GotCamera();
        base.PickUpItem();
        StartCoroutine(NextInteract());
    }

    private IEnumerator NextInteract()
    {
        yield return new WaitForSeconds(4);
        base.Interact();
        yield return new WaitForSeconds(3);
        _balloon.SetActive(true);
        base.Interact();
    }
}
