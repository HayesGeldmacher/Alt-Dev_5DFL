using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightManager : Interactable
{


    public float _neededRooms = 10;
    public float _currentRooms = 0;
    public bool _enteredRightRoom = false;
    private bool _hasSpawnedExit = false;

    [SerializeField] private newKitchenCollider _triggerKitchen;

    [SerializeField] private PlayerController _controller;
    [SerializeField] private List<GameObject> _Items = new List<GameObject>();
    [SerializeField] private List<AudioClip> _soundClips = new List<AudioClip>();
    [SerializeField] private int _itemNum;
    [SerializeField] private AudioSource _audio;

    [SerializeField] private GameObject _face;
    [SerializeField] private GameObject _TV;
    [SerializeField] private AudioSource _turnOffSound;
    [SerializeField] private ScreenshotHandler _handler;

    [SerializeField] private GameObject _flag1;
    [SerializeField] private GameObject _flag2;

    [SerializeField] private AudioSource _spotLight;
    [SerializeField] private AudioSource _interactAudio;

    [SerializeField] private bool _testDontFreeze = false;
    [SerializeField] private GameObject _remoteEvidence;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private Transform _newSpawnPos;

    private bool _seenDialogue = false;

    [SerializeField] private GameObject _endlessKitchen;
    [SerializeField] private GameObject _normalHouse;
    [SerializeField] private Animator _kitchenLight;
    [SerializeField] private Animator _stairWellLight;
    [SerializeField] private Datamosh _data;
    [SerializeField] private CharacterController _char;
    [SerializeField] private Animator _garageLight;

    [SerializeField] private CameraController _camController;
    [SerializeField] private GameObject _kitchenFakeEvidence;


    [Header("New Night Evidence Variables")]
    [SerializeField] private int _evidenceCount;
    [SerializeField] private int _totalEvidenceNeeded = 4;
    [SerializeField] private GameObject _newPhone;
    [SerializeField] private GameObject _oldPhone;
    private bool _completedEvidence = false;
    [SerializeField] private AudioSource _publicAudio;

    [SerializeField] private CameraZoom _camZoom;



    // private float _disableController = false;
    // Start is called before the first frame update
    void Start()
    {

        //TeleportPlayer();
        
        if (!_testDontFreeze)
        {
        _controller._frozen = true;

        }
        _itemNum = 0;


        _camController.GotCamera();

        _evidenceCount = 0;


        // _audio.clip = _soundClips[0];

        float _countNum = 0;
        foreach(var item in _Items)
        {
              //  item.SetActive(false);
             //   _countNum ++;
        }

        StartCoroutine(StartDialogue());
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && _seenDialogue)
        {
            base.Interact();
            _interactAudio.Play();
            _seenDialogue = false;
            StartCoroutine(SpawnFlags());
        }


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
    
    private IEnumerator SpawnFlags()
    {

        yield return new WaitForSeconds(0.2f);
        _flag1.SetActive(true);
        _flag2.SetActive(true);
    }


    private IEnumerator StartDialogue()
    {
        _flag1.SetActive(false);
        _flag2.SetActive(false);
        yield return new WaitForSeconds(1f);
        base.Interact();
        _seenDialogue = true;
    }

    public void FreePlayer()
    {
        _controller._frozen = false;
    }

    public void AddRoom(newKitchenCollider _tempKitchen)
    {
       
        if(_tempKitchen == _triggerKitchen)
        {

        }
        else
        {

            if(_currentRooms >= _neededRooms)
            {
                SpawnExit();
            }       
            _currentRooms++;
            Debug.Log("Added A Room!");
        }

        _triggerKitchen = _tempKitchen;
    }

    private void SpawnExit()
    {
        if (!_hasSpawnedExit)
        {
            Debug.Log("SpawnedExit");
            _hasSpawnedExit= true;
            _remoteEvidence.SetActive(true);

        }
    }

    public void CallExitKitchen()
    {
        StartCoroutine(ExitKitchen());       
    }

    private IEnumerator ExitKitchen()
    {
        _kitchenLight.SetTrigger("fade");
        yield return new WaitForSeconds(2f);
        Destroy(_kitchenFakeEvidence);
        TeleportPlayer();
        yield return new WaitForSeconds(0.1f);
        _normalHouse.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _endlessKitchen.SetActive(false);
        _stairWellLight.SetTrigger("appear");
        yield return new WaitForSeconds(1.5f);
        _garageLight.SetTrigger("appear");
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
        Debug.Log("newPhoneDONE!");
        _oldPhone.SetActive(false);
        _newPhone.SetActive(true);
        _newPhone.transform.GetComponent<PhoneNight1>().StartRinging();
    }

    public void PlaySound(AudioClip _clip)
    {
        _publicAudio.clip = _clip;
        _publicAudio.Play();
    }
}
