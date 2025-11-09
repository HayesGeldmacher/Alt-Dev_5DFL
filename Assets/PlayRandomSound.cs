using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : Interactable
{

    [SerializeField] private AudioSource _audio;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start(); 
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();
        _audio.pitch = Random.Range(0.8f, 1.2f);
        _audio.Play();
    }
}
