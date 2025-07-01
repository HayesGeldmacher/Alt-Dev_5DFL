using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LampIteration
{
    public GameObject iteration;

    public bool _playBreathingSound;

    public AudioSource _source;
    


    public void Activate(bool active)
    {

        if (active)
        {
            iteration.SetActive(true);
            if (_playBreathingSound)
            {
                _source = iteration.transform.GetComponent<AudioSource>();
                _source.Play();
            }
        }
        else
        {
            iteration.SetActive(false);
            if (_playBreathingSound)
            {
                _source.Stop();
            }
        }
    }
}
