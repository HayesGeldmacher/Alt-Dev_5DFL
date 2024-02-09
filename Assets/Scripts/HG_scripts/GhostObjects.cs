using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObjects : MonoBehaviour
{
    public bool _visible;
    
    public virtual void Appear()
    {
        
        Debug.Log("FUCK!");
        _visible = true;
    }

    public virtual void Disappear()
    {
        Debug.Log("FUCK DISAP");
        _visible = false;
    }
}
