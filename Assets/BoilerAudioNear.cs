using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilerAudioNear : MonoBehaviour
{

    [SerializeField] private Transform _playerBody;
    [SerializeField] private LayerMask _playerMask;

    [SerializeField] private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = _playerBody.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, _playerMask))
        {
            if (hit.transform.tag == "Player")
            {
                if (!_audio.isPlaying)
                {
                    _audio.Play();
                }
            }
            else
            {
                if (_audio.isPlaying)
                {
                    _audio.Pause();
                }
            }
        }
        else
        {
            if (_audio.isPlaying)
            {
                _audio.Pause();
            }
        }
    }
}
