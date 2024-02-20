using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _flashLight;
    [SerializeField] private AudioSource _interactAudio;

    public void PlayClick()
    {
        if (!_flashLight.isPlaying)
        {
            _flashLight.Play();

        }   
    }

    public void PlayInteract()
    {
        _interactAudio.pitch = Random.Range(0.9f, 1.1f);
        _interactAudio.Play();
    }
}
