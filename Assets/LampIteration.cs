using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LampIteration
{
    public GameObject[] _disappearObjectsOn;
    public GameObject[] _appearObjectsOn;

    public GameObject[] _disappearObjectsOff;
    public GameObject[] _appearObjectsOff;

    public bool _playBreathingSound;
    public bool _stopBreathingSound;
    
    public void Activate(bool turnOn)
    {
        if (turnOn)
        {
            foreach (GameObject disappear in _disappearObjectsOn)
            {
                
            }

        }
    }
}
