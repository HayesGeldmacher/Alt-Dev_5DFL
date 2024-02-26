using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //This script is a parent script that many objects will inherit from


    [Header("Dialogue Variables")]
    [SerializeField] protected bool _canTalk = false;
    [HideInInspector] public bool _startedTalking = false;
    [SerializeField] private bool _canWalkAway;
    [SerializeField] private float _dialogueTimer = 1;
    [SerializeField] private float _dialogueDistance = 5;
    private DialogueManager _manager;
    public Dialogue _dialogue;

    [Header("Item Variables")]
    [SerializeField] private bool _canBeGrabbed;

    [Header("Outline Variables")]
    [SerializeField] private bool _outlined;
    [HideInInspector] public bool _isOutlined = false;
    

    protected Transform _player;

    protected void Start()
    {
        _manager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
    }

    private void Update()
    {
       if(_canWalkAway && _startedTalking)
        {
            float _distance = Vector3.Distance(_player.position, transform.position);
            if(_distance > _dialogueDistance)
            {
                EndDialogue();
            }

        }
    }

    //writing "virtual" in front of a function means that children scripts can add to/edit the function
    public virtual void Interact()
    {
        if (_canTalk)
        {
            TriggerDialogue();
        }

        //Adds an outline to object when interacting, if we set the bool 
      

    }
    
    public virtual void OnOutline()
    {
        if (_outlined)
        {
            _isOutlined = true;
            int _outlineLayer = LayerMask.NameToLayer("evidence");
            gameObject.layer = _outlineLayer;

        }
    }
    public virtual void StopInteract()
    {
        if (_outlined)
        {
            _isOutlined = false;
            int _interactableLayer = LayerMask.NameToLayer("Interactable");
            gameObject.layer = _interactableLayer;
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

    public void PickUpItem()
    {
        //have all the pickup stuff happen here
        if (_canTalk)
        {
            _manager.CallTimerEnd(_dialogueTimer);
        }
        Destroy(gameObject);

    }

   
}
