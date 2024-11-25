using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveInteract : Interactable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _screechSound;
    [SerializeField] private GameObject _grandHallGate;
    
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
        _anim.SetTrigger("turn");
        _screechSound.Play();
        _grandHallGate.SetActive(false);
    }
}
