using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private AudioSource _thump;
    

    public void PlayThump()
    {
        if (!_thump.isPlaying)
        {
        _thump.pitch = Random.Range(0.8f, 1.1f);
        _thump.Play();

        }
    }

}
