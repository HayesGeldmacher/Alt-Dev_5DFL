using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private float[] _audioLevels;
    private int _currentAudio = 0;
    [SerializeField] private bool _fading = false;
    [SerializeField] private float _fadeSpeed;



    // Update is called once per frame
    void Update()
    {
        if (_fading)
        {
            foreach (AudioSource source in _audioSources)
            {
                float volumeLimit = _audioLevels[_currentAudio];
                if (source.volume > volumeLimit)
                {
                    source.volume += (_fadeSpeed * Time.deltaTime) / 10;
                }

                _currentAudio++;
            }
        }
    }

    public void StartFading()
    {
        _fading = true;
    }
}
