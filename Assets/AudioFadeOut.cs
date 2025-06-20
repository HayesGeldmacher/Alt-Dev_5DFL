using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{

    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private bool _fading = false;
    [SerializeField] private float _fadeSpeed;



    // Update is called once per frame
    void Update()
    {
        if (_fading)
        {
            foreach(AudioSource source in _audioSources)
            {
                if(source.volume > 0)
                {
                    source.volume -= (_fadeSpeed * Time.deltaTime) / 10;
                }
            }
        }
    }

    public void StartFading()
    {
        _fading = true;
    }
}
