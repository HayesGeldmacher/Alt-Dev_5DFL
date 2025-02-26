using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarNight : MonoBehaviour
{

    [SerializeField] private AudioSource _stringSound;
    [SerializeField] private AudioSource _stringSound2;
    private bool _playFirst = true;
    [SerializeField] private AudioClip[] _stringClips;

    private bool _playSounds = true;

    [SerializeField] private float _minWait;
    [SerializeField] private float _maxWait;
    [SerializeField] private float _currentWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (_playSounds)
        {
            _currentWaitTime -= Time.deltaTime;

            if(_currentWaitTime <= 0)
            {
                PlayNote();
            }
        }
        




    }

    private void PlayNote()
    {
        AudioSource newSource;
        if(_playFirst)
        {
            newSource = _stringSound;
        }
        else
        {
           newSource = _stringSound2;   
        }

        int clipNum = Random.Range(0, _stringClips.Length);
        AudioClip newClip = _stringClips[clipNum];
        
         newSource.clip = newClip;
         newSource.volume = Random.Range(0.8f, 1f);

         newSource.Play();
        _playFirst = !_playFirst;

        _currentWaitTime = Random.Range(_minWait, _maxWait);
    }

    public void EnableSound()
    {
        _playSounds = true;
    }
}
