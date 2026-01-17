using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CamChangePositions : MonoBehaviour
{

    [SerializeField] private Transform _cam;
    [SerializeField] public SingleMosh _mosh;


    //Adding list that we can call for each trigger!
    public CamChangeTrigger[] _triggers;

    public Transform[] _location;
    public int _currentCam;
    public Interactable _interact;

    [Header("Video Fields")]
    public VideoPlayer _player;
    public Animator _vidAnimator;

    public CamChangeTrigger _camChange;


    //This singleton creates a locatable script instance that can be located easily from any other script!
    #region Singleton

    public static CamChangePositions instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Manager is present! NOT GOOD!");
            return;
        }

        instance = this;
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //just for testing...
        // _camChange.CallGeneric();
        //ChangePos(0);
        CallTrigger(0);


        //StartCoroutine(TutorialMessage());
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CallTrigger(int newPos)
    {
        _triggers[newPos].CallGeneric();
    }

    public void ChangePos(int newPos)
    {
        _mosh.CallGlitch();
        _currentCam = newPos;
        _cam.position = _location[newPos].position;
        _cam.rotation = _location[newPos].rotation;


    }

    private IEnumerator TutorialMessage()
    {
        yield return new WaitForSeconds(1.5f);
        _interact.Interact();
        GameManager.instance.PlayInteractSound();
    }

    public void CallVideo(float cutTime, VideoClip clip)
    {
        StartCoroutine(VideoCut(cutTime, clip));
    }

    private IEnumerator VideoCut(float cutTime, VideoClip clip)
    {
        _player.clip = clip;
        _player.Play();
        _vidAnimator.SetTrigger("play");
        yield return new WaitForSeconds(cutTime);
        _vidAnimator.SetTrigger("stop");
        _mosh.CallGlitch();

    }
}
