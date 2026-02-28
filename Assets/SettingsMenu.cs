using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    //for all audio
    public AudioMixer masterMixer;

    //for idle background audio
    public AudioMixer idleMixer;

    //for interact sounds
    public AudioMixer interactAudio;

    //for loud noises, jumpscares!
    public AudioMixer scareAudio;

    //for music 
    public AudioMixer musicAudio;

    public void SetVolume(float volume)
    {
        masterMixer.SetFloat("volume", volume);
    }


}
