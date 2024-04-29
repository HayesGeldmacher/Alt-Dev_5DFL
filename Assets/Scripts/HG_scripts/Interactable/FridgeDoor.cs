using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : Interactable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _swing;
    private bool _isOpen = false;
    public override void Interact()
    {
        base.Interact();
        if (_isOpen)
        {
            _isOpen = false;
            _anim.SetTrigger("close");
           /// _swing.Play();
        }
        else
        {
            _isOpen = true;
            _anim.SetTrigger("open");
          ///  _swing.Play();
        }
    }

    
}
