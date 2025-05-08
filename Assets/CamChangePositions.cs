using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangePositions : MonoBehaviour
{

    [SerializeField] private Transform _cam;

    public Transform[] _location;
    public int _currentCam;


    // Start is called before the first frame update
    void Start()
    {
        //just for testing...
        //ChangePos(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangePos(int newPos)
    {
        _currentCam = newPos;
        _cam.position = _location[newPos].position;
        _cam.rotation = _location[newPos].rotation;

    }
}
