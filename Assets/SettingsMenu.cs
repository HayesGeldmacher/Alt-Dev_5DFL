using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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


    public void Start()
    {
        SetAudioPrefs();
        SetCameraPrefs();
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
        if (PlayerPrefs.HasKey("cameraSpeedX")){
            float value = PlayerPrefs.GetFloat("cameraSpeedX");
            camSliderX.value = (value);
            Debug.Log("Set cam speed x volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for camera speed X!");
        }

        if (PlayerPrefs.HasKey("cameraSpeedY")){
            float value = PlayerPrefs.GetFloat("cameraSpeedY");
            camSliderX.value = (value);
            Debug.Log("Set cam speed y in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for camera speed Y!");
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
}
