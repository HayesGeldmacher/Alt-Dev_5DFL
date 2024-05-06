using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;

    [SerializeField] private float _minRot;
    [SerializeField] private float _maxRot;

    private Vector3 _startingAngles;
    
    // Start is called before the first frame update
    void Start()
    {
      _startingAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
      
        transform.LookAt(_player, _player.rotation * Vector3.up);
        Vector3 rotation = new Vector3(transform.localEulerAngles.x, -transform.localEulerAngles.y, -transform.localEulerAngles.z);
       transform.localEulerAngles = rotation;
    }
}
