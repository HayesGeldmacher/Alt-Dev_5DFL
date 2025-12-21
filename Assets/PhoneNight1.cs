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
    public Dialogue _endDialogue;
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

    public bool openDoor = true;
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

    [SerializeField] private List<GameObject> _disappearObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _appearObjects = new List<GameObject>();
    [SerializeField] private bool _disappear;
    [SerializeField] private bool _appear;


    [SerializeField] private GameObject _newDoorExit;
    private bool _endedFirstTime = false;
    [SerializeField] private AudioSource _doorOpenSound;


    [SerializeField] private Animator _phoneLightAnim;
    [SerializeField] private AudioSource _phonePutDown;

    [Header("FMV Fields")]
    [SerializeField] private bool doFMV = false;
    [SerializeField] private Animator FMVAnim;
    [SerializeField] private bool _interactingInFMV;
    [SerializeField] private Animator cursorAnim;
    [SerializeField] private float _totalWaitTime = 2f;
    [SerializeField] private float _currentWaitTime = 0;
    [SerializeField] private AudioSource _creepyLaughing;
    [SerializeField] private GameObject _newPhone;
    private float oldWaitTime = -1;
    private bool _finishedFirstTime = false;
    public bool _lineDead = false;
    

    private void Start()
    {
        //base.Start();
        base.Start();_currentDialogueLine = 0;
        _totalDialogue = base._dialogue._sentences.Length;
        _dialogueManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _defaultDialogue = base._dialogue;

        if (_phoneLightAnim != null)
        {
            _phoneLightAnim.SetTrigger("on");
        }

       
    }

    private void Update()
    {

        base.Update();

        if (_interactingInFMV)
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.instance._isPaused)
            {
               
                if(_currentWaitTime <= 0)
                {
                    Interact();
                    _currentWaitTime = _totalWaitTime;
                    if (cursorAnim != null)
                    {
                        cursorAnim.SetTrigger("click");
                        cursorAnim.SetBool("appear", false);
                    }
                }
            }



            _currentWaitTime -= Time.deltaTime;
            if(_currentWaitTime <= 0)
            {
                if(oldWaitTime > 0)
                {
                    cursorAnim.SetBool("appear", true);
                }
            }


            oldWaitTime = _currentWaitTime;

        }


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

        Debug.Log("Interacted with Phone!!");
        if (_finishedFirstTime && _lineDead)
        {
            
            TriggerDialogue(_endDialogue);
            return;
        }
        
        
        if (_ringSound.isPlaying)
        {
            _ringSound.Stop();

            if (_phoneLightAnim != null)
            {
                _phoneLightAnim.SetTrigger("talking");
               // _cam._frozen = true;
            }
        }


        if(_currentDialogueLine >= _totalDialogue)
        {
             EndDialogue();
           
           _finishedFirstTime = true;
            Debug.Log("ended Dialogue!");
            _currentDialogueLine = 0;
            if(FMVAnim != null)
            {
             FMVAnim.SetTrigger("disappear");
            }

            if (doFMV)
            {
                StartCoroutine(DisableSelf());
            }
            _cam._frozen = false;
            _interactingInFMV = false;
            if (cursorAnim != null) {
                cursorAnim.SetBool("appear", false);
            }

            if(_creepyLaughing != null)
            {
                _creepyLaughing.Stop();
            }
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
                if (doFMV)
                {
                    FMVAnim.SetTrigger("appear");
                    _cam._frozen = true;
                    _interactingInFMV = true;
                    if (cursorAnim != null)
                    {
                        //cursorAnim.SetBool("appear", true);
                    }

                }
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

        if (_disappear)
        {
            foreach(GameObject _item in _disappearObjects)
            {
                if(_item != null)
                {
                    _item.SetActive(false);
                }
            }
        }

        if (_appear)
        {
            foreach(GameObject _item in _appearObjects)
            {
                if(_item != null)
                {
                    _item.SetActive(true);
                }
            }
        }

    }


   private IEnumerator OpenDoor()
    {

        yield return new WaitForSeconds(1.5f);
        if (_newDoorExit != null)
        {
            
        _newDoorExit.GetComponent<Door>().SetDirection();
        
            if(_doorOpenSound != null)
            {
                _doorOpenSound.Play();
            }

        }
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

        if (!_endedFirstTime)
        {
            _endedFirstTime = true;
            if (!doFMV)
            {
                StartCoroutine(OpenDoor());
                Debug.Log("opened Door!");
            }
        }

        if(_phoneLightAnim != null)
        {
            _phoneLightAnim.ResetTrigger("on");
            _phoneLightAnim.SetTrigger("off");
        }

        _phonePutDown.Play();

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


    public void StartRinging()
    {
        _ringSound.Play();
        if (_phoneLightAnim != null)
        {
            _phoneLightAnim.SetTrigger("on");
        }
    }


    private IEnumerator DisableSelf()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        _newPhone.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
