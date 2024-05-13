using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Interactable
{
    
    
    [SerializeField] private Animator _letterAnim;
    public float speed;
    private DialogueManager _dialogueManager;
    private Dialogue _defaultDialogue;
    public Dialogue _phoneDialogue;
    [SerializeField] private EvidenceManager _evidence;
    [SerializeField] private Animator _anim;
    private float t;
    float duration = 4;
    private bool _hasSpawned = false;
    private int diaNum = 0;
    [SerializeField] private bool _canAudio = true;
    private bool _hasPlayed = false;

    private void Start()
    {
        //base.Start();
        base.Start();
        _dialogueManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _defaultDialogue = base._dialogue;
    }

    private void TriggerRead()
    {

    }


    private void Update()
    {

        base.Update();
        if (base._startedTalking && base._isTimed)
        {

        }

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
        base.currentDialogueTime = base._dialogueTimer;

        if (_evidence._hasEvidence)
        {
            TriggerDialogue(_phoneDialogue);
            diaNum += 1;

            if (diaNum == 1)
            {
               
            }
            else if (diaNum == 2)
            {
                
            }
            else if (diaNum == 3)
            {
              
            }
        }
        else
        {

            TriggerDialogue(_defaultDialogue);
        }
    }

    private void TriggerDialogue(Dialogue _dialogue)
    {
        if (_dialogue == _phoneDialogue)
        {
            _evidence._hasFoundPhone = true;

        }

        if (!base._startedTalking)
        {
            _hasPlayed = false;
            if (_dialogue == _phoneDialogue)
            {
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

                _anim.SetTrigger("fade");
                _hasPlayed = true;

            }

        }
    }

    public override void EndDialogue()
    {
        _dialogueManager.EndDialogue();

    }

}

