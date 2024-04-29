using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonNight : ShootTrigger
{
    [SerializeField] private AudioSource _scream;
    [SerializeField] private GameObject _bloodStains;
    [SerializeField] private MeshRenderer _render;
    
    public override void Interact()
    {
        _scream.Play();
        _bloodStains.SetActive(true);
        Destroy(gameObject);
    }

}
