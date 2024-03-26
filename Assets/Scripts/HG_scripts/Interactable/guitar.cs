using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guitar : Interactable
{
    [SerializeField] private AudioSource _guitarSound1;
    [SerializeField] private AudioSource _guitarSound2;
    [SerializeField] private List<AudioClip> _guitarSounds = new List<AudioClip>();
    private int _soundCount;
    private bool _playFirst = true;
   public override void Start()
    {
        base.Start();
        _soundCount = 0;
    }

   public override void Interact()
    {
        //base.Interact();
        PlaySound();
        

    }

    private void PlaySound()
    {
       
        if (_playFirst)
        {
            _guitarSound1.clip = _guitarSounds[_soundCount];
            _guitarSound1.pitch = Random.Range(0.8f, 1.2f);
            _guitarSound1.Play();
        }
        else
        {
            _guitarSound2.clip = _guitarSounds[_soundCount];
            _guitarSound2.pitch = Random.Range(0.8f, 1.2f);
            _guitarSound2.Play();
        }
        _playFirst = !_playFirst;
        _soundCount ++;
        if(_soundCount >= _guitarSounds.Count)
        {
            _soundCount = 0;
        }
    }
}
