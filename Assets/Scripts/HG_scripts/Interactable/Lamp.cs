using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{

    [SerializeField] private GameObject _lampHeadDim;
    [SerializeField] private GameObject _lampHeadLit;
    
    public override void Interact()
    {
        GameObject _light = transform.GetChild(0).gameObject;
        if(_light.activeInHierarchy)
        {
            _light.SetActive(false);
            _lampHeadDim.SetActive(true);
            _lampHeadLit.SetActive(false);
        }
        else
        {
            _light.SetActive(true);
            _lampHeadDim.SetActive(false);
            _lampHeadLit.SetActive(true);
        }
    }
}
