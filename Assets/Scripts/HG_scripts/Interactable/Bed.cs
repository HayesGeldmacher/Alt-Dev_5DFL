using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    private DialogueManager _bedManager;
    private Dialogue _bedDialogue;
   

    private void Start()
    {
        _bedManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _bedDialogue = base._dialogue;
    }

    private void Update()
    {
        if (base._canWalkAway && base._startedTalking)
        {
            float _distance = Vector3.Distance(_player.position, transform.position);
            if (_distance > base._dialogueDistance)
            {
                EndDialogue();
            }

        }
    }

    //writing "virtual" in front of a function means that children scripts can add to/edit the function
    public override void Interact()
    {
        if (base._canTalk)
        {
            TriggerDialogue();

        }

        //Adds an outline to object when interacting, if we set the bool 


    }

    public override void TriggerDialogue()
    {

        if (!base._startedTalking)
        {
            Interactable _interactable = transform.GetComponent<Interactable>();
            _bedManager.StartDialogue(_bedDialogue, _interactable);
            base._startedTalking = true;
        }
        else
        {
            _bedManager.DisplayNextSentence();
        }
    }

    public override void EndDialogue()
    {
        _bedManager.EndDialogue();
    }



}
