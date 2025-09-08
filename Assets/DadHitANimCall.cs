using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadHitANimCall : MonoBehaviour
{
    public AudioSource _thumpSound;
    
    public void CallThump()
    {
        _thumpSound.pitch = Random.Range(0.8f, 1.2f);
        _thumpSound.Play();
    }
}
