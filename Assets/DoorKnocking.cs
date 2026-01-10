using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnocking : MonoBehaviour
{

    [SerializeField] private AudioClip[] knockClips;
    [SerializeField] private AudioSource source;
    [SerializeField] private float minWait;
    [SerializeField] private float maxWait;

    [SerializeField] float currentWait = 4;
    private bool looping = true;
    [SerializeField] bool counting = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!looping) { return; }
        if (!counting)
        {
            if (!source.isPlaying)
            {
                counting = true;
            }
            return;
        }
        currentWait -= Time.deltaTime;
        
        if(currentWait <= 0)
        {
            counting = false;
            PlayAudio();
            currentWait = Random.Range(minWait, maxWait);
        }
    }

    private void PlayAudio()
    {
        AudioClip audio = knockClips[Random.Range(0, knockClips.Length)];
        source.clip = audio;
        source.pitch = Random.Range(0.6f, 0.8f);
        source.Play();
    }

    public void StopAudio()
    {
        source.Stop();
        looping = false;
    }

    public void ResumeAudio()
    {
        looping = true;
    }

    public void SetIntensity(float newMinWait, float newMaxWait)
    {
        minWait = newMinWait;
        maxWait = newMaxWait;
    }
}
