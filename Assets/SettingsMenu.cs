using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{

    //for all audio
    public AudioMixer masterMixer;

    //for idle background audio
    public AudioMixer idleMixer;

    //for interact sounds
    public AudioMixer interactMixer;

    //slider for general volume
    public Slider masterSlider;

    //slider for idle/background volume
    public Slider idleSlider;

    //slider for interaction volume
    public Slider interactSlider;

    //sliders for camera sensitivity x and y axes
    public Slider camSliderX;
    public Slider camSliderY;

    public Toggle invertToggle;

    public bool useWorldBrightness = false;
    public Slider worldBrightness;
    public PostProcessProfile worldBrightnessProfile;
    public AutoExposure exposure;

    public void Start()
    {
        if (useWorldBrightness)
        {
            worldBrightnessProfile.TryGetSettings(out exposure);

        }
        
        
       AssignPreferences();
    }

    public void AssignPreferences()
    {
        SetAudioPrefs();
        SetCameraPrefs();
        SetCamPrefs();
        SetBrightnessPrefs();
    }

    //sets the audio sliders to the current mixer values
    public void SetAudioPrefs()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("volume");
            masterSlider.value = (masterVolume);
            Debug.Log("Set master volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for master volume!");
        }

        if (PlayerPrefs.HasKey("idleVolume"))
        {
            float idleVolume = PlayerPrefs.GetFloat("idleVolume");
            idleSlider.value = (idleVolume);
            Debug.Log("Set idle volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for idle volume!");
        }

        if (PlayerPrefs.HasKey("interactVolume"))
        {
            float interactAudio = PlayerPrefs.GetFloat("interactVolume");
            interactSlider.value = (interactAudio);
            Debug.Log("Set interact volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for interact volume!");
        }
    }

    public void SetCameraPrefs()
    {
        if (PlayerPrefs.HasKey("cameraSpeedX"))
        {
            float value = PlayerPrefs.GetFloat("cameraSpeedX");
            camSliderX.value = (value);
            Debug.Log("Set cam speed x volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for camera speed X!");
            camSliderX.value = 1.0f;

        }
        if (PlayerPrefs.HasKey("cameraSpeedY"))
        {
            float value = PlayerPrefs.GetFloat("cameraSpeedY");
            camSliderY.value = (value);
            Debug.Log("Set cam speed Y volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for camera speed Y!");
            camSliderY.value = 1.0f;

        }
    }

    public void SetCamPrefs()
    {
        if (PlayerPrefs.HasKey("camInvert"))
        {
            int toggled = PlayerPrefs.GetInt("camInvert");
            if (toggled == -1)
            {
                invertToggle.isOn = true;
            }
            else
            {
                invertToggle.isOn = false;
            }
        }
    }

    public void SetBrightnessPrefs()
    {
        if (PlayerPrefs.HasKey("worldBrightness"))
        {
            float value = PlayerPrefs.GetFloat("worldBrightness");
            worldBrightness.value = (value);
            Debug.Log("Set world brightness volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists world brightness!");
            worldBrightness.value = 1.0f;

        }
    }

    public void SetGeneralVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        masterMixer.SetFloat("volume", volume);
    }

    public void SetIdleVolume(float volume)
    {
        PlayerPrefs.SetFloat("idleVolume", volume);
        idleMixer.SetFloat("idleVolume", volume);
    }
    
    public void SetInteractVolume(float volume)
    {
        PlayerPrefs.SetFloat("interactVolume", volume);
        interactMixer.SetFloat("interactVolume", volume);
    }

    public void SetCamMultiplierX(float multiplier)
    {
        PlayerPrefs.SetFloat("cameraSpeedX",  multiplier);
    }

    public void SetCamMultiplierY(float multiplier)
    {
        PlayerPrefs.SetFloat("cameraSpeedY", multiplier);
    }

    public void SetCamToggle(bool toggle)
    {
        
        if(toggle == true)
        {
            PlayerPrefs.SetInt("camInvert",  -1);
            Debug.Log("Set cam toggle to true!");
        }
        else
        {
            Debug.Log("Set cam toggle to false!");
            PlayerPrefs.SetInt("camInvert", 1);
   
        }
    }

    public void SetWorldBrightness(float brightness)
    {
        PlayerPrefs.SetFloat("worldBrightness", brightness);
        if(exposure != null)
        {
            exposure.keyValue.value = brightness;

        }

    }

    public void SetDefaults()
    {
        SetGeneralVolume(1.0f);
        SetIdleVolume(1.0f);
        SetInteractVolume(1.0f);
        SetCamMultiplierX(1.0f);
        SetCamMultiplierY(1.0f);
        SetCamToggle(false);
        SetWorldBrightness(1.0f);

        AssignPreferences();
    }
}
