using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyWallHitDoor : Interactable
{
    private AudioSource _wallHit;
    private bool _hasHit = false;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _wallHit = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (!_hasHit)
        {
        _wallHit.Play();
            _hasHit = true;
        }
        base.Interact();
    }
}
