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

    [SerializeField] private FridgeDoor _fridge;
    [SerializeField] private GameObject _monster;
    [SerializeField] private GameObject _light;
    [SerializeField] private Transform _monsterLocation;
    [SerializeField] private Key _key;
    [SerializeField] private Door _door;
    [SerializeField] private AudioSource _doorAudio;
    [SerializeField] private Door _doorDad;
    [SerializeField] private GameObject _lightObject;
    [SerializeField] private Door _doorBathroom;
    [SerializeField] private GameObject _bathroomLight;
    [SerializeField] private Transform _monsterFaceDirection;
    [SerializeField] private GameObject _bathroomLight2;
    [SerializeField] private GameObject _baseFloorLamp;
     private bool _hasSpawned = false;

    private bool _hasPlayed = false;

     private void Start()
    {
        //base.Start();
        base.Start();
        _dialogueManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _defaultDialogue = base._dialogue;
        _bathroomLight2.SetActive(false);
    }

    private void Update()
    {
     
        base.Update();
        if(base._startedTalking && base._isTimed)
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
                if (!_hasSpawned)
                {
                    CallKitchen();
                   
                }
            }

        }  

    }

    public override void EndDialogue()
    {
        _dialogueManager.EndDialogue();
        
    }

    public void CallSpawnMonster()
    {
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        _hasSpawned = true;
        Debug.Log("spawned Enemy");
        yield return new WaitForSeconds(1.5f);
        _monster.transform.position = _monsterLocation.position;

        Vector3 relativePos = _monsterFaceDirection.position - _monster.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        _monster.transform.rotation = rotation;
        _monster.SetActive(false);


        //Sets the door locked 
        _door.SetDirection();
        
        //Opens the dads door
        if(_doorDad._isOpen)
        {
        _doorDad.SetDirection();
           
        }
        _doorDad.SetDirection();
        
        _door._isLocked = true;
        _key.gameObject.SetActive(true);
        _door._key = _key;
        _door._keyName = _key.gameObject.name.ToString();
        yield return new WaitForSeconds(2f);
        _doorAudio.Play();
        _lightObject.SetActive(false );
        
        
    }

    public void CallSpawnBathroom()
    {
        StartCoroutine(SpawnBathroom());
    }
    private IEnumerator SpawnBathroom()
    {
        _bathroomLight.SetActive(true);
        _bathroomLight2.SetActive(true);
        _baseFloorLamp.SetActive(false);
        if (!_doorBathroom._isOpen)
        {
            _doorBathroom.SetDirection();
        }

        _monster.SetActive(true);
        _light.SetActive(false);
        yield return new WaitForSeconds(1);
    }

    public void CallKitchen()
    {
        _fridge._canSwing = true;
        _light.SetActive(true);
        
    }
}
