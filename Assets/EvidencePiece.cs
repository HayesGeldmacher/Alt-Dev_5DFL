using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class EvidencePiece : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private bool _canSound = true;
    [SerializeField] private float _minRange = 5;
    private Transform _player;
    private bool _inRange;
    private bool _canPlay;
    private float _currentTime;
    private GameManager _manager;
    private AudioSource _audio;

    [Header("Breathing")]
    [SerializeField] private bool _canBreathe;
    [SerializeField] private float _sizeMult = 1.05f;
    [SerializeField] private float _breatheSpeed = 2;
    [SerializeField] private Transform _scaleModel;
    private bool _isGrowing = true;
    private Vector3 _startSize;
    private Vector3 _bigSize;
    private float _growTime;

    

    // Start is called before the first frame update
    void Start()
    {
        if (_canBreathe)
        {
            if(_scaleModel == null)
            {
                _scaleModel = transform;
            }
            _startSize = _scaleModel.localScale;
            _bigSize.x = _startSize.x * _sizeMult;
            _bigSize.y = _startSize.y * _sizeMult;
            _bigSize.z = _startSize.z * _sizeMult;

            _growTime = 2;
        }

        if(_canSound)
        {
            _player = PlayerController.instance.transform;
            _minRange = 5;
            _manager = GameManager.instance;
            _currentTime = 0;
            _audio = GetComponent<AudioSource>();
            var audioClip = Resources.Load<AudioClip>("EvidenceJingle");
            _audio.clip = audioClip;
            _audio.spatialBlend = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (_canSound)
        {
            SoundUpdate();
        }

        if (_canBreathe)
        {
            BreatheUpdate();
        } 
    }

    private void SoundUpdate()
    {
        float _distance = Vector3.Distance(_player.position, transform.position);
        if (_distance <= _minRange)
        {
            _inRange = true;
        }
        else
        {
            _inRange = false;
        }

        if (!_canPlay)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                _canPlay = true;
            }
        }

        if (_inRange && _canPlay)
        {
            PlaySound();
        }

    }

    private void BreatheUpdate()
    {
        Vector3 _currentSize = _scaleModel.localScale;
        float X = _currentSize.x;
        float Y = _currentSize.y;
        float Z = _currentSize.z;
        

        if(_isGrowing)
        {
            X = Mathf.Lerp(X, _bigSize.x, _breatheSpeed * Time.deltaTime);
            Y = Mathf.Lerp(Y, _bigSize.y, _breatheSpeed * Time.deltaTime);
            Z = Mathf.Lerp(Z, _bigSize.z, _breatheSpeed * Time.deltaTime);

        }
        else
        {
            X = Mathf.Lerp(X, _startSize.x, _breatheSpeed * Time.deltaTime);
            Y = Mathf.Lerp(Y, _startSize.y, _breatheSpeed * Time.deltaTime);
            Z = Mathf.Lerp(Z, _startSize.z, _breatheSpeed * Time.deltaTime);
        }

        _growTime -= Time.deltaTime;
        if(_growTime <= 0)
        {
            _isGrowing = !_isGrowing;
            _growTime = 2;
        }
        
        _currentSize = new Vector3(X, Y, Z);

        _scaleModel.localScale = _currentSize;
    }

    private void PlaySound()
    {
        _canPlay = false;
        _currentTime = 3;
        _audio.Play();
    }
}
