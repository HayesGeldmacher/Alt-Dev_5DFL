using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class EvidencePiece : MonoBehaviour
{

    private Transform _player;
    private bool _inRange;
    private bool _canPlay;
    private float _currentTime;
    private GameManager _manager;
    [SerializeField] private float _minRange = 5;
    private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerController.instance.transform;
        _minRange = 5;
        _manager = GameManager.instance;
        _currentTime = 0;

        _audio = GetComponent<AudioSource>();
        var audioClip = Resources.Load<AudioClip>("EvidenceJingle");
        _audio.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        float _distance = Vector3.Distance(_player.position, transform.position);
        if(_distance <= _minRange)
        {
            _inRange = true;
        }
        else
        {
            _inRange = false;
        }

        if(!_canPlay)
        {
            _currentTime -= Time.deltaTime;
            if(_currentTime <= 0)
            {
                _canPlay = true;
            }
        }
        
        if(_inRange && _canPlay)
        {
            PlaySound();
        }
        
    }

    private void PlaySound()
    {
        _canPlay = false;
        _currentTime = 3;
        _audio.Play();
    }
}
