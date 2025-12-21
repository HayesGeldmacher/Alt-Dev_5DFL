using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDreamPosition : Interactable
{

    public GameObject[] appearObjects;
    public GameObject[] disappearObjects;

    [Header("Anim Fields")]
    public bool _animatesFace = false;
    public Animator _faceAnim;
    public int animNum = 0;
    public int maxAnimNum = 0;
    public bool startedAnim = false;
    public AudioSource knockingSound;
    float knockingVolume = 0;

    [Header("WallFaceSounds")]
    public AudioClip[] clips;
    public AudioSource source;
    public AudioSource idleSource;
    public AudioSource TVsound;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void CallPositionShot()
    {

        base.Interact();

        if (_animatesFace)
        {
                animNum++;
            if (!startedAnim)
            {
                //start anim
                _faceAnim.SetTrigger("open");
                startedAnim = true;
                if(knockingSound != null)
                {
                    knockingVolume = knockingSound.volume;
                    knockingSound.volume = 0;
                }
                source.clip = clips[Random.Range(0,2)];
                source.pitch = Random.Range(0.8f, 1.1f);
                source.Play();
                
                idleSource.Play();
                TVsound.Stop();
            }
            else
            {
                if(animNum <= maxAnimNum)
                {
                    _faceAnim.SetTrigger("continue");
                    source.clip = clips[Random.Range(0, 2)];
                    source.pitch = Random.Range(0.8f, 1.1f);
                    source.Play();
                }
                else
                {
                    _faceAnim.SetTrigger("close");
                    idleSource.loop = false;
                    knockingSound.volume = knockingVolume;
                    source.clip = clips[Random.Range(0, 2)];
                    source.pitch = Random.Range(0.8f, 1.1f);
                    source.Play();
                    if (disappearObjects.Length > 0)
                    {
                        DisappearObjects();
                    }
                    if (appearObjects.Length > 0)
                    {
                        AppearObjects();
                    }
                    TVsound.Play();
                    StaticDreamManager.instance.NextShot(transform);
                }
                //continue anim
            }
        }
        else
        {
            //go to next shot
            if (disappearObjects.Length > 0)
            {
                DisappearObjects();
            }
            if(appearObjects.Length > 0)
            {
                AppearObjects();
            }
            StaticDreamManager.instance.NextShot(transform);
        }


    }

    private void DisappearObjects()
    {
        foreach(GameObject thing in disappearObjects)
        {
            thing.SetActive(false);
        }
    }

    private void AppearObjects()
    {
        foreach(GameObject thing in appearObjects)
        {
            thing.SetActive(true);
        }
    }
}
