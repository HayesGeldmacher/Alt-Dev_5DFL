using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CamChangeLighting : CamChangeTrigger
{

    [SerializeField] private bool _light = false;
    [SerializeField] private float _lightValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public override void CallGeneric()
    {
        base.CallGeneric();
        ChangeLighting();
    }

    private void ChangeLighting()
    {
        if (_light)
        {
            RenderSettings.ambientIntensity = _lightValue;
        }
    }
}
