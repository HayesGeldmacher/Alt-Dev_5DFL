using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParentAnimPicker : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private int _spinAnim;
    // Start is called before the first frame update
    void Start()
    {
        _anim.SetInteger("spin", _spinAnim);
    }


}
