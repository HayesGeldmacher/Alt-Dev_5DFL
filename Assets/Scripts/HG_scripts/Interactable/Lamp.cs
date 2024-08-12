using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    public override void Interact()
    {
        GameObject _light = transform.GetChild(0).gameObject;
        if(_light.activeInHierarchy)
        {
            _light.SetActive(false);
        }
        else
        {
            _light.SetActive(true);
        }
    }
}
