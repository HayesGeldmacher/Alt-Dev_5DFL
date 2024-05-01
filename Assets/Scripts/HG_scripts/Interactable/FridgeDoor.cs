using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeDoor : Interactable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _swing;
    public bool _canSwing = false;
    private bool _isOpen = false;
    public override void Interact()
    {
      if(_canSwing)
        {
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
        else
        {
        base.Interact();

        }
    }

    private void Update()
    {
        base.Update();
    }

    private void Start()
    {
        base.Start();
    }
    
}
