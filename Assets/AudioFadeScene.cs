using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeScene : MonoBehaviour
{

    [Header("Audio Fade Fields")]
    [SerializeField] private AudioSource[] fadeSources;

    [SerializeField] private float[] maxFadeVolume;

    [SerializeField] private float[] fadeSpeed;

    [SerializeField] private bool[] fadingUp;

    public bool fadeAudioUp = true;
    public bool fadeAudioDown = false;
    public int done = 0;

    // Start is called before the first frame update
    void Start()
    {
        fadeAudioUp = true;

        foreach(var fade in fadeSources)
        {
            if(fade != null)
            {
                fade.volume = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(done == fadeSources.Length)
        {
            fadeAudioUp = false;
            fadeAudioDown = false;
        }

        if (fadeAudioUp) {

            int i = 0;
            
            foreach(AudioSource source in fadeSources)
            {

               if(source!= null)
                {
                    if (source.volume <= maxFadeVolume[i] && fadingUp[i])
                    {
                        float newVolume = source.volume + Time.deltaTime * fadeSpeed[i];
                        source.volume  = newVolume;
                    }
                    else
                    {
                        fadingUp[i] = false;
                        done++;
                    }

                }
                else
                {
                    done++;
                }
                    i++;
            }
            
        }
        else if(fadeAudioDown)
        {
            int i = 0;

            foreach (AudioSource source in fadeSources)
            {

                if (source != null)
                {
                    if (source.volume >= maxFadeVolume[i] && !fadingUp[i])
                    {
                        float newVolume = source.volume - Time.deltaTime * fadeSpeed[i];
                        source.volume = newVolume;
                    }
                    else
                    {
                        fadingUp[i] = true;
                        done++;
                    }

                }
                else
                {
                    done++;
                }
                i++;
            }
        }
    }
}
