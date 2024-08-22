using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinAnims : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private int _animChoice;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _anim = transform.GetComponent<Animator>();
        _anim.SetInteger("animChoice", _animChoice);
    }


    private void StartFade()
    {
        _anim.SetInteger("animChoice", 4);
    }

}
