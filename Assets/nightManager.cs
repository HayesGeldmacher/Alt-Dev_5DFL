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

    [SerializeField] private GameObject _flag1;
    [SerializeField] private GameObject _flag2;

    [SerializeField] private AudioSource _spotLight;
    [SerializeField] private AudioSource _interactAudio;
    private bool _seenDialogue = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller._frozen = true;
        _itemNum = 0;
       // _audio.clip = _soundClips[0];

        float _countNum = 0;
        foreach(var item in _Items)
        {
                item.SetActive(false);
                _countNum ++;
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

        if (_itemNum < _Items.Count)
        {
             _audio.Play();
            StartCoroutine(NextItemGo());
           

        }
        else
        {
            _handler.CallSetMonster();
            _TV.SetActive(false);
            _turnOffSound.Play();
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

}
