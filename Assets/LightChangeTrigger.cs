using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChangeTrigger : MonoBehaviour
{


    [SerializeField] private float _lightValue;
    public lightingThirdPerson _lightThirdPerson;
    [SerializeField] private Interactable _interact;
    private bool _interacted = false;


    public void OnTriggerEnter(Collider other)
    {
        
            if (other.tag == "Player")
            {
            _lightThirdPerson._lightValue = _lightValue;
                _lightThirdPerson._lerping = true;

                if(_interact != null)
                {
                    if (!_interacted)
                    {
                        
                    _interact.Interact();
                    _interacted = true;
                    GameManager.instance.PlayInteractSound();

                    }    
                }
           
            }
        

    }


   
}
