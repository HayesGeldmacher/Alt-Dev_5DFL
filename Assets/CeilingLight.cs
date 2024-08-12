using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLight : MonoBehaviour
{

    [SerializeField] private GameObject _light;
    [SerializeField] private GameObject _bulb; 
    
    public void Flip(bool On)
    {
        if (On)
        {
         _light.SetActive(true);
            _bulb.SetActive(true);

        }
        else
        {
            _light.SetActive(false);
            _bulb.SetActive(false);
        }
    }
}
