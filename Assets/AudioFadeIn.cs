using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private float[] _audioLevels;
    public int _currentAudio = 0;
    [SerializeField] private bool _fading = false;
    [SerializeField] private float _fadeSpeed;

    public bool justUsingOne = false;

    // Update is called once per frame
    void Update()
    {
        if (_fading)
        {

            if (justUsingOne)
            {
                AudioSource source = _audioSources[0];
                float volumeLimit = _audioLevels[0];
                if (source.volume < volumeLimit)
                {
                    source.volume += (_fadeSpeed * Time.deltaTime) / 10;
                }

            }
            else
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
    }

    public void StartFading()
    {
        _fading = true;
    }
}
