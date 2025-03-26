using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BoilerAudioNear : MonoBehaviour
{

    [SerializeField] private Transform _playerBody;
    [SerializeField] private LayerMask _playerMask;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _currentAudio;
    [SerializeField] private float _audioDecreaseRate;
    [SerializeField] private bool _inSight;

    // Start is called before the first frame update
    void Start()
    {
        _currentAudio = _audio.volume;
    }

    // Update is called once per frame
    void Update()
    {
        _currentAudio = _audio.volume;
        Vector3 direction = _playerBody.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, _playerMask))
        {
            if (hit.transform.tag == "Player")
            {
                _inSight = true;
                IncreaseUpdate();
            }
            else
            {
                _inSight = false;
                DecreaseUpdate();
            }
        }
        else
        {
            _inSight = false;
            DecreaseUpdate();
        }
    }

    private void DecreaseUpdate()
    {
        float currentVol = _currentAudio;
        if(_currentAudio > 0f)
        {
            currentVol -= (1 * _audioDecreaseRate * Time.deltaTime);
        }

        _audio.volume = currentVol;
    }

    private void IncreaseUpdate()
    {
        float currentVol = _currentAudio;
        if (_currentAudio < 1)
        {
            currentVol += (1 * _audioDecreaseRate * Time.deltaTime);
        }

        _audio.volume = currentVol;
    }


}
