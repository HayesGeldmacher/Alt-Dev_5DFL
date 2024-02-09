using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMesh : GhostObjects
{
   
    public override void Appear()
    {
        base.Appear();
        transform.GetComponent<MeshRenderer>().enabled = true;
    }

    public override void Disappear()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;
    }
}
