using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampMini : Interactable
{

    [SerializeField] private GameObject[] _disappearObjects;
    [SerializeField] private GameObject[] _appearObjects;

    public bool _activated;
    
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

        base.Interact();
        _activated = !_activated;
        foreach(GameObject thing in _disappearObjects)
        {
            thing.SetActive(!_activated);
        }

        foreach(GameObject thing in _appearObjects)
        {
            thing.SetActive(_activated);
        }
    }
}
