using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDreamPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPositionShot()
    {
        StaticDreamManager.instance.NextShot(transform);
    }
}
