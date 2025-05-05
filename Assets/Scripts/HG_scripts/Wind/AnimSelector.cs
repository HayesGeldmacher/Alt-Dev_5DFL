using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(Animator))]
public class AnimSelector : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private int _animNum;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        if(_anim != null)
        {
            _anim.SetInteger("num", _animNum);
        }
    }

}
