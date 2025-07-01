using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LampIteration
{
    public GameObject iteration;

    public bool _playBreathingSound;
    public bool _stopBreathingSound;
    
    public void Activate(bool active)
    {

        if (active)
        {
            iteration.SetActive(true);
        }
        else
        {
            iteration.SetActive(false);
        }
    }
}
