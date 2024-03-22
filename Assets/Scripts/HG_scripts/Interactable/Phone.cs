using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : Interactable
{

    private DialogueManager _dialogueManager;
    private Dialogue _defaultDialogue;
    public Dialogue _phoneDialogue;
    [SerializeField] private EvidenceManager _evidence;
    [SerializeField] private AudioSource _garble1;
    [SerializeField] private AudioSource _garble2;

    private bool _hasPlayed = false;

     private void Start()
    {
        _dialogueManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _defaultDialogue = base._dialogue;
    }

    private void Update()
    {
        if (base._canWalkAway && base._startedTalking && _player)
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

        if (_evidence._hasEvidence)
        {       
           TriggerDialogue(_phoneDialogue);      
        }
        else
        {
         
            TriggerDialogue(_defaultDialogue);
        }
    }

    private void TriggerDialogue(Dialogue _dialogue)
    {
        if (_dialogue ==  _phoneDialogue)
        {
            _evidence._hasFoundPhone = true;
            
        }

        if (!base._startedTalking)
        {
            _hasPlayed = false;
            if(_dialogue == _phoneDialogue)
            {
                _garble1.Play();
            }

            Interactable _interactable = transform.GetComponent<Interactable>();
            _dialogueManager.StartDialogue(_dialogue, _interactable);
            base._startedTalking = true;
        }
        else
        {
           _dialogueManager.DisplayNextSentence();

            if (_dialogue == _phoneDialogue && !_hasPlayed)
            {
                _garble2.Play();
                _hasPlayed = true;
            }

        }

    }

    public override void EndDialogue()
    {
        _dialogueManager.EndDialogue();
    }

}
