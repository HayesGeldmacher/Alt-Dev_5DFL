using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTelevision : MonoBehaviour
{

    public Animator _wallFade;

    // Start is called before the first frame update
    void Start()
    {
        _wallFade.SetTrigger("fade");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
