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

    [SerializeField] private GameObject _monster;
    [SerializeField] private GameObject _light;
    [SerializeField] private Transform _monsterLocation;
    [SerializeField] private Key _key;
    [SerializeField] private Door _door;
 
    private bool _hasSpawned = false;

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
            if (!_hasSpawned)
            {
                StartCoroutine(SpawnMonster());
            }
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

    private IEnumerator SpawnMonster()
    {
        _hasSpawned = true;
        Debug.Log("spawned Enemy");
        yield return new WaitForSeconds(1);
        _monster.transform.position = _monsterLocation.position;

        Vector3 relativePos = _player.transform.position - _monster.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        _monster.transform.rotation = rotation;

        _light.SetActive(true);

        //Sets the door locked 
        if (_door._isOpen)
        {
            _door.SetDirection();
        }
        _door._isLocked = true;
        _key.gameObject.SetActive(true);
        _door._key = _key;
        _door._keyName = _key.gameObject.name.ToString();
        

    }
}
