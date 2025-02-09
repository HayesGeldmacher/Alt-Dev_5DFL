using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingChair : Interactable
{

    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _couchTalk;
    
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
        _anim.SetTrigger("talk");
        
        _couchTalk.pitch = Random.Range(0.8f, 1.2f);
        _couchTalk.Play();
    }
}
