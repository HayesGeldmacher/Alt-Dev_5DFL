using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumButton : Interactable
{
    [SerializeField] private int _number;
    [SerializeField] private Animator _anim;
    [SerializeField] private GunSafe _gunSafe;

    int _count = 0;
    
    public override void Interact()
    {
        base.Interact();
        _anim.SetTrigger("pressed");
        _gunSafe.ButtonPressed(_number);
    }
}
