using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TitleSetPlayerrPrefs : MonoBehaviour
{

    //sits in the title screen and loads basic setttings according to player prefs
    //for all audio
    public AudioMixer masterMixer;

    //for idle background audio
    public AudioMixer idleMixer;

    //for interact sounds
    public AudioMixer interactMixer;


    // Start is called before the first frame update
    void Start()
    {
        SetAudioPrefs();
    }

   public void SetAudioPrefs()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("volume");
            masterMixer.SetFloat("volume", masterVolume);
            Debug.Log("Set master volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for master volume!");
        }

        if (PlayerPrefs.HasKey("idleVolume"))
        {
            float idleVolume = PlayerPrefs.GetFloat("idleVolume");
            idleMixer.SetFloat("idleVolume", idleVolume);
            Debug.Log("Set idle volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for idle volume!");
        }

        if (PlayerPrefs.HasKey("interactVolume"))
        {
            float interactAudio = PlayerPrefs.GetFloat("interactVolume");
            interactMixer.SetFloat("interactVolume", interactAudio);
            Debug.Log("Set interact volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for interact volume!");
        }
   }
}
