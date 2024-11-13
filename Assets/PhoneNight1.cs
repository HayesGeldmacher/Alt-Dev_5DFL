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
    private bool _interactedLast = false;


    [Header("NewNightStuff")]
    private bool _interactedFirst = false;
    private bool _canInteract = true;
    [SerializeField] private GameObject _wallApparition;
    [SerializeField] private GameObject _doorWall;
    [SerializeField] private int _dialogueCount;
    [SerializeField] private int _currentDialogueLine = 0;
    [SerializeField] private List<AudioClip> _gargleSounds = new List<AudioClip>();
    [SerializeField] private bool _gargles = false;
    [SerializeField] private AudioSource _gargleSource;
    [SerializeField] private float _totalDialogue = 5;

    [Header("DisappearShit")]
    [SerializeField] private GameObject _showerBlocker;
    [SerializeField] private GameObject _stairWellBlocker;
    [SerializeField] private GameObject _showerDoorAppear;
    [SerializeField] private GameObject _HallwayBedroomBlocker;




    private void Start()
    {
        //base.Start();
        base.Start();_currentDialogueLine = 0;
        _totalDialogue = base._dialogue._sentences.Length;
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

    public override void Interact()
    {
        
        if (_ringSound.isPlaying)
        {
            _ringSound.Stop();
        }

        //if (!_canInteract) return;



        if(_currentDialogueLine >= _totalDialogue)
        {
             EndDialogue();
            Debug.Log("ended Dialogue!");
            _currentDialogueLine = 0;
        }
        else
        {
        base.Interact();
        base.currentDialogueTime = base._dialogueTimer;
       // TriggerDialogue(_defaultDialogue);
        _controller._frozen = true;

            if (!_interactedFirst)
            {
                _interactedFirst = true;
                DisappearHouse();
            }


            if (_gargles)
            {
                PlaySound();
                Debug.Log("PLAYED SOUND!");
            }
            _currentDialogueLine += 1;

        }

        //check if dialogue


    }

    private void DisappearHouse()
    {
        _showerDoorAppear.SetActive(true);
        _stairWellBlocker.SetActive(false);
        _showerBlocker.SetActive(false);
        _HallwayBedroomBlocker.SetActive(false);

    }


   


    private void TriggerDialogue(Dialogue _dialogue)
    {
        Interactable _interactable = transform.GetComponent<Interactable>();

        if (!base._startedTalking)
        {
        base._startedTalking = true;
        _dialogueManager.StartDialogue(_dialogue, _interactable);

        }
        else
        {
         _dialogueManager.DisplayNextSentence();
        }

    }

    public override void EndDialogue()
    {

        Debug.Log("ENDED!");
        _dialogueManager.EndDialogue();
        _cam._canInteract = true;
        _controller._frozen = false;

    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        _canInteract = true;

    }

    private void PlaySound()
    {
        
        int _length = _gargleSounds.Count;
        Debug.Log("SOUND NUMBA: " +  _length + " DIALOGUE NUMBA: " + _currentDialogueLine);

        if((_currentDialogueLine + 1) <= _length)
        {

        AudioClip _clip = _gargleSounds[_currentDialogueLine];
        _gargleSource.clip = _clip;
        _gargleSource.Play();

        }
        
    }

}
