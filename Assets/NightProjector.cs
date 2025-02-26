using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NightProjector : MonoBehaviour
{

    [SerializeField] private GameObject _bike;

    [SerializeField] private VideoClip[] _clips;
    [SerializeField] private VideoClip _bikeClip;
    [SerializeField] private VideoPlayer _player;
    [SerializeField] private int _currentClip = 0;
    private bool _isBike = false;
    private bool _started = false;

    // Start is called before the first frame update
    void Start()
    {
        _currentClip = 0; 
    }


    private void Update()
    {


            if (!_player.isPlaying)
            {
                if (_isBike)
                {
                    DisableBike();
                }
                else
                {
                    EnableBike();
                }
            }
        
        
    }



    public void EnableBike()
    {
        _isBike = true;
        _player.clip = _bikeClip;
        _bike?.SetActive(true);
        _player.Play();
    }

    public void DisableBike()
    {
        _isBike = false;

        _player.clip = _clips[_currentClip];
        

        if (_currentClip >= _clips.Length - 1)
        {
            _currentClip = 0;
        }
        else
        {
            _currentClip ++;
        }

        _player.Play();
        _bike?.SetActive(false);
    }


    
}
