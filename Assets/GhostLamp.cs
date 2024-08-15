using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLamp : Interactable
{

    [SerializeField] private GameObject _ghost;
    
    public override void Interact()
    {
      _ghost.SetActive(false);

        GameObject _light = transform.GetChild(0).gameObject;
        if (_light.activeInHierarchy)
        {
            _light.SetActive(false);
        }
        else
        {
            _light.SetActive(true);
        }
    }
}
