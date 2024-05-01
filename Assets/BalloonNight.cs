using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonNight : ShootTrigger
{
    [SerializeField] private AudioSource _scream;
    [SerializeField] private GameObject _bloodStains;
    [SerializeField] private Phone _phone;
    
    public override void Interact()
    {
        _bloodStains.SetActive(true);
        Destroy(gameObject);
        _phone.CallSpawnMonster();
        _scream.Play();
    }

}
