using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioSource _interactionSounds;
    

    public void PlayClick()
    {
        if (!_interactionSounds.isPlaying)
        {
            _interactionSounds.clip = _click;
            _interactionSounds.Play();

        }   
    }
}
