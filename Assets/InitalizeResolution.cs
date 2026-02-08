using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitalizeResolution : MonoBehaviour
{
    
    //on awake, force resolution 
    private void Awake()
    {
        Screen.SetResolution(800, 600, true);
    }
    

}
