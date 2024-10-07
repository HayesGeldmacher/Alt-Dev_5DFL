using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatCallNoise : MonoBehaviour
{
    [SerializeField] private AudioSource _beatNoise1;
    [SerializeField] private AudioSource _beatNoise2;
    private bool _playFirst = true;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallBeat()
    {
        if (_playFirst)
        {
            PlayBeat(_beatNoise1);
        }
        else
        {
            PlayBeat(_beatNoise2);
        }

        _playFirst = !_playFirst;
    }


    private void PlayBeat(AudioSource _audio)
    {
        _audio.Play();
    }
}
