using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetAnimCaller : MonoBehaviour
{
    public PuppetTelevision _puppetTelevision;

    public void CallRoad()
    {
        _puppetTelevision.FadeOutPuppet();
    }
}
