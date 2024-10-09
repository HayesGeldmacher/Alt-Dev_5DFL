using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneNight1 : Interactable
{
    [SerializeField] private GameObject _cameraPickup;
    public float speed;
    [SerializeField] public bool _isDarkening = false;
    [SerializeField] public Color colorStart = Color.blue;
    [SerializeField] public Color colorEnd = Color.green;
    private DialogueManager _dialogueManager;
    [SerializeField] private SoundManager _interactAudio;

    private Dialogue _defaultDialogue;
    public Dialogue _phoneDialogue;
    public Dialogue _phoneDialogueMorning;
    [SerializeField] private EvidenceManager _evidence;
    [SerializeField] private AudioSource _garble1;
    [SerializeField] private AudioSource _garble2;
    [SerializeField] private AudioSource _garble3;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _lightShaft1;
    [SerializeField] private GameObject _lightShaft2;
    [SerializeField] private Material _skyBoxMat;
    [SerializeField] private GameObject _pointWindowLight;
    [SerializeField] private GameObject _sun;
    private float t;
    float duration = 4;
    private bool _hasSpawned = false;
    [SerializeField] private int diaNum = 0;
    [SerializeField] private bool _canAudio = true;
    private bool _hasPlayed = false;
    public bool _doneMorning = false;
    public bool _startMorning = false;
    public float _currentMorningDialogue = 0;
    public float _totalMorningDialogue;
    [SerializeField] BedDayTime _bed;

    [SerializeField] private PlayerController _controller;
    [SerializeField] private CameraController _cam;

    [SerializeField] private AudioSource _ringSound;

    [SerializeField] private GameObject _tvBroken;
    [SerializeField] private GameObject _tvWorking;


    [Header("NewNightStuff")]
    private bool _interactedFirst = false;
    private bool _interactedLast = false;
    private bool _canInteract = true;
    [SerializeField] private GameObject _wallApparition;
    [SerializeField] private GameObject _doorWall;




    private void Start()
    {
        //base.Start();
        base.Start();
        _dialogueManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _defaultDialogue = base._dialogue;
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
        if (_ringSound.isPlaying)
        {
            _ringSound.Stop();
        }

        if (!_canInteract) return;

        base.currentDialogueTime = base._dialogueTimer;


        if (!_interactedLast)
        {
            if (_interactedFirst)
            {
                TriggerDialogue(_phoneDialogueMorning);
                _startMorning = true;
                _currentMorningDialogue += 1;

                _controller._frozen = true;

            }
            else
            {
               
                
                
                
                _currentMorningDialogue += 1;
                if (_currentMorningDialogue >= _totalMorningDialogue)
                {
                    EndDialogue();
                }
                else
                {
                    _dialogueManager.DisplayNextSentence();

                }

            }
        }

        else if (_evidence._hasEvidence)
        {

            diaNum += 1;
            if (diaNum >= 5)
            {
                EndDialogue();
                diaNum = 0;
            }
            else
            {
                _controller._frozen = true;
                TriggerDialogue(_phoneDialogue);

            }


        }
        else
        {

            TriggerDialogue(_defaultDialogue);
        }
    }

    private void TriggerDialogue(Dialogue _dialogue)
    {


        if (!_doneMorning)
        {
            Interactable _interactable = transform.GetComponent<Interactable>();
            _dialogueManager.StartDialogue(_dialogue, _interactable);
        }
        if (!_doneMorning) return;


        if (_dialogue == _phoneDialogue)
        {
            _evidence._hasFoundPhone = true;

        }

        if (!base._startedTalking)
        {
            _hasPlayed = false;
            if (_dialogue == _phoneDialogue)
            {
                if (_garble1)
                {
                    // _garble1.Play();
                }
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
                if (_garble2)
                {
                    _garble2.Play();
                }
                _hasPlayed = true;

            }

        }

    }

   


    public override void EndDialogue()
    {

        Debug.Log("ENDED!");
        _dialogueManager.EndDialogue();
        _cam._canInteract = true;
        _controller._frozen = false;

        if (!_doneMorning)
        {
            _canInteract = false;
            StartCoroutine(WaitTime());
            _doneMorning = true;
            _cameraPickup.SetActive(true);
        }

    }

    private void SunDown()
    {

    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        _canInteract = true;

    }

}
