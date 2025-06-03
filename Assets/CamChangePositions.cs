using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangePositions : MonoBehaviour
{

    [SerializeField] private Transform _cam;
    [SerializeField] private SingleMosh _mosh;
    public Transform[] _location;
    public int _currentCam;
    public Interactable _interact;

    // Start is called before the first frame update
    void Start()
    {
        //just for testing...
        ChangePos(0);
        StartCoroutine(TutorialMessage());
       
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
