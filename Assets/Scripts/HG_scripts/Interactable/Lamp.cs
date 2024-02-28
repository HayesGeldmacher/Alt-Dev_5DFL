using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    public override void Interact()
    {
        Light _light = transform.GetChild(0).GetComponent<Light>();
        if(_light.enabled == true)
        {
            _light.enabled = false;
        }
        else
        {
            _light.enabled = true;
        }
    }
}
