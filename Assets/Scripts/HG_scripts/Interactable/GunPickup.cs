using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : Interactable
{

    [SerializeField] private GameObject _revolver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        _revolver.SetActive(true);
        Destroy(gameObject);
    }
}
