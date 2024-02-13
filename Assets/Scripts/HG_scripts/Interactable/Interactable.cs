using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //This script is a parent script that many objects will inherit from

    [SerializeField] protected bool _canTalk = false;
    public Dialogue _dialogue;
    private DialogueManager _manager;
    [HideInInspector] public bool _startedTalking = false;

    private void Start()
    {
        _manager = GameManager.instance.GetComponent<DialogueManager>();
    }

    //writing "virtual" in front of a function means that children scripts can add to/edit the function
    public virtual void Interact()
    {

        if (_canTalk)
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
      
        if (!_startedTalking)
        {
            _manager.StartDialogue(_dialogue, this);
            _startedTalking = true;
        }
        else
        {
            _manager.DisplayNextSentence();
        }
    }

    private void EndDialogue()
    {
        _manager.EndDialogue();
    }
}
