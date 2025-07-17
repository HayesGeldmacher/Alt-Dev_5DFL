using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetAudio : MonoBehaviour
{
    public AudioClip[] _puppetLaughs;
    public AudioClip[] _puppetTalks;

    public AudioSource _audio;

    private int _currentLaugh = 0;
    private int _currentTalk = 0;

    public void PlayTalK()
    {
        if (_currentTalk >= _puppetTalks.Length)
        {
            _currentTalk = 0;
        }

        
        _audio.clip = _puppetTalks[_currentTalk];
        _audio.pitch = Random.Range(0.85f, 1.15f);
        _audio.Play();
        _currentTalk++;
    }

    public void PlayLaugh()
    {
        if (_currentLaugh >= _puppetLaughs.Length)
        {
            _currentLaugh = 0;
        }
        
        _audio.clip = _puppetLaughs[_currentLaugh];
        _audio.pitch = Random.Range(0.85f, 1.15f);
        _audio.Play();
        _currentLaugh++;
    }
 }
