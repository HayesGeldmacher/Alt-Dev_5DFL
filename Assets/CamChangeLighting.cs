using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CamChangeLighting : CamChangeTrigger
{

    [SerializeField] private bool _light = false;
    [SerializeField] private float _lightValue;
    [SerializeField] private bool _playSound = false;
    [SerializeField] private bool _stopSound = false;
    [SerializeField] private AudioSource _sound;

    [SerializeField] private bool _changeFog = false;
    [SerializeField] private float _fogDensity;

    public bool _changeFogColor = false;
    public Color _fogColor;

    [SerializeField] private bool _changeVol;
    [SerializeField] private float _soundVol;

    [SerializeField] private bool _changePan;
    [SerializeField] private float _soundPan;

   

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


        if (_playSound)
        {
            ChangeSound(true);
        }
        else if (_stopSound)
        {
            ChangeSound(false);
        }

        if (_changeVol)
        {
            ChangeVolume(_soundVol);
        }

        if (_changeFog)
        {
            RenderSettings.fogDensity = _fogDensity;
        }

        if (_changeFogColor)
        {
            RenderSettings.fogColor = _fogColor;
        }


        if (_changePan)
        {
            _sound.spatialBlend = _soundPan;
        }

        
    }


    private void ChangeVolume(float vol)
    {
        _sound.volume = vol;
    }

    private void ChangeSound(bool play)
    {
        if (play)
        {
            _sound.Play();
        }
        else
        {
            _sound.Stop();
        }
    }

    private void ChangeLighting()
    {
        if (_light)
        {
            RenderSettings.ambientIntensity = _lightValue;
        }
    }

  
}
