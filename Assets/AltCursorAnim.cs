using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltCursorAnim : MonoBehaviour
{
    [SerializeField] private Animator _cursorAnim;
    
    public void EnableCursorAnim()
    {
        _cursorAnim.SetBool("appear", true);
    }

    public void DisableCursorAnim() 
    {
        _cursorAnim.SetBool("appear", false);
    }
}
