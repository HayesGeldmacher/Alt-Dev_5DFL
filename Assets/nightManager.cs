using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightManager : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        _controller._frozen = true;
        _itemNum = 0;
       // _audio.clip = _soundClips[0];

        float _countNum = 0;
        foreach(var item in _Items)
        {
            if(_countNum != 0)
            {
                item.SetActive(false);
            }

            _countNum ++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextItem()
    {

        _handler.CallEvidenceDing();
        _audio.clip = _soundClips[_itemNum];
        
        _itemNum++;

        if (_itemNum < _Items.Count)
        {
            _Items[_itemNum].SetActive(true);
            _audio.Play();

        }
        else
        {
            _handler.CallSetMonster();
            _TV.SetActive(false);
            _turnOffSound.Play();
        }
    }


    


}
