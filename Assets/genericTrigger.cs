using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericTrigger : Interactable
{

    private bool _hasEncountered = false;

    public bool changeController = false;
    public GameInputManager gameInputManager;
    public Dialogue controllerDialogue;

    private void Start()
    {
        base.Start();
    }
    
    private void Update()
    {
        base.Update();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!_hasEncountered)
        {

            if (other.gameObject.tag == "Player")
            {
              if(changeController && !gameInputManager._usingMouse)
                {
                    base._dialogue = controllerDialogue;
                }
                
                _hasEncountered = true;
                base.Interact();
            }
        }
    }
}
