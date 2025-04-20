using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _player;

    [SerializeField] private float _minRotX;
    [SerializeField] private float _maxRotX;

    [SerializeField] private bool _limitRotation = false;

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

        if (_limitRotation)
        {
            rotation = new Vector3(Mathf.Clamp(rotation.x, _minRotX, _maxRotX), rotation.y, rotation.z);
        }

       transform.localEulerAngles = rotation;
    }
}
