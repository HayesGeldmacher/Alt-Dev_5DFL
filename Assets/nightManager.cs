using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightManager : Interactable
{


    [SerializeField] private PlayerController _controller;
    [SerializeField] private List<GameObject> _Items = new List<GameObject>();
    [SerializeField] private List<AudioClip> _soundClips = new List<AudioClip>();
    [SerializeField] private int _itemNum;
    [SerializeField] private AudioSource _audio;

    [SerializeField] private GameObject _face;
    [SerializeField] private GameObject _TV;
    [SerializeField] private AudioSource _turnOffSound;
    [SerializeField] private ScreenshotHandler _handler;

    [SerializeField] private AudioSource _spotLight;
    [SerializeField] private AudioSource _interactAudio;

    [SerializeField] private bool _testDontFreeze = false;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private Transform _newSpawnPos;

    private bool _seenDialogue = false;



    [SerializeField] private Datamosh _data;
    [SerializeField] private CharacterController _char;

    [SerializeField] private CameraController _camController;


    [Header("New Night Evidence Variables")]
    [SerializeField] private int _evidenceCount;
    [SerializeField] private int _totalEvidenceNeeded = 4;
    [SerializeField] private GameObject _newPhone;
    [SerializeField] private GameObject _oldPhone;
    private bool _completedEvidence = false;
    [SerializeField] private AudioSource _publicAudio;

    [SerializeField] private CameraZoom _camZoom;

    private bool _phoneCompletedDialogue = false;
    private bool _phoneEndedDialogue = false;
    [SerializeField] private GameObject _telephoneHallwayBlocker;
    [SerializeField] private GameObject _bedroomHallwayBlocker;
    [SerializeField] private List<GameObject> _disappearComplete;
    [SerializeField] private List<GameObject> _appearComplete;
    public Interactable dialogueInteract;

   // [SerializeField] private float _totaldialogueTimer;
   //[SerializeField] private float _currentDialogueTimer;

    // private float _disableController = false;
    // Start is called before the first frame update
    void Start()
    {

        //TeleportPlayer();
        
        if (!_testDontFreeze)
        {
        _controller._frozen = true;

        }
        //_itemNum = 0;


        _camController.GotCamera();

        _evidenceCount = 0;

        //just for testing
        //StartCoroutine(StartDialogue());
      // StartCoroutine(EvidenceCompleteDialogue());
        base.Start();
        //CompleteEvidence();

    }

    // Update is called once per frame
    void Update()
    {
       base.Update();

        /*
         if (Input.GetButtonDown("Interact"))
         {
             CallEvidenceCompleteDialogue();
             _interactAudio.Play();
         }
         return;


         if(Input.GetButtonDown("Interact") && _seenDialogue)
         {
             dialogueInteract.Interact();
             _interactAudio.Play();
             _seenDialogue = false;
         }

        
        else if (Input.GetMouseButtonDown(0) && _phoneCompletedDialogue && !_phoneEndedDialogue)
        {
            _phoneEndedDialogue = true;
            dialogueInteract.Interact();
            _interactAudio.Play();

        }
         */


    }

    public void NextItem()
    {

       
        _handler.CallEvidenceDing();
        _audio.clip = _soundClips[_itemNum];
        
        _itemNum++;
        _audio.Play();

        if (_itemNum < _Items.Count)
        {
            //StartCoroutine(NextItemGo());
           

        }
        else
        {
           // _handler.CallSetMonster();
            //_TV.SetActive(false);
           // _turnOffSound.Play();
        }
    }

    private IEnumerator NextItemGo()
    {
        yield return new WaitForSeconds(5f);
        _Items[_itemNum].SetActive(true);
        _spotLight.Play();
    }

    public void SpawnFirst()
    {
        _spotLight.Play();
        _Items[0].SetActive(true);
    }
    
   


    private IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);
        dialogueInteract.Interact();
        _seenDialogue = true;
    }

    public void CallEvidenceCompleteDialogue()
    {
        StartCoroutine(EvidenceCompleteDialogue());
    }

    private IEnumerator EvidenceCompleteDialogue()
    {
        yield return new WaitForSeconds(2); 
        dialogueInteract._dialogue._sentences[0] = "The phone is ringing again...";
        dialogueInteract.Interact();
        _phoneCompletedDialogue = true;
    }

    public void FreePlayer()
    {
        _controller._frozen = false;
    }



    public void CallDataMosh()
    {
        _data.Glitch();
        _data.Glitch();
        _data.Glitch();
    }


    private void TeleportPlayer()
    {
        _camZoom.TurnOffFlash();
        _playerParent.SetActive(false);
        //_controller._frozen = true;
        //_char.enabled = false;
        _playerParent.transform.position = _newSpawnPos.position;
        //Debug.Break();
        _playerParent.SetActive(true);
        _char.enabled = true;
        _controller._frozen = false;

    }



    public void CollectEvidence()
    {
        _evidenceCount++;
        if (_evidenceCount >= _totalEvidenceNeeded)
        {
            if (!_completedEvidence)
            {
                _completedEvidence = true;
                CompleteEvidence();
            }
        }
    }

    private void CompleteEvidence()
    {
        CallEvidenceCompleteDialogue();
        Debug.Log("newPhoneDONE!");
        _oldPhone.SetActive(false);
        _newPhone.SetActive(true);
        _newPhone.transform.GetComponent<PhoneNight1>().StartRinging();

        if(_telephoneHallwayBlocker != null)
        {
            _telephoneHallwayBlocker.SetActive(false);
        }
        if(_bedroomHallwayBlocker != null)
        {
                _bedroomHallwayBlocker.SetActive(true);

        }

        foreach(GameObject obj in _disappearComplete)
        {
            if(obj != null)
            {
                Destroy(obj);
            }
        }

        foreach(GameObject obj in _appearComplete)
        {
            if(obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    public void PlaySound(AudioClip _clip)
    {
        _publicAudio.clip = _clip;
        _publicAudio.Play();
    }
}
