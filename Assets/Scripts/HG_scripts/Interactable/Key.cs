using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public string _keyName;
     
    
    // Start is called before the first frame update
    void Start()
    {
        _keyName = gameObject.name.ToString();   
    }

    public override void Interact()
    {
        base.Interact();
        CameraController _camera = Camera.main.GetComponent<CameraController>();
        _camera._keys.Add(_keyName);
        Destroy(gameObject);
    }
}
