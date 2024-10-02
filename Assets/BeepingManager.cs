using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeepingManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _searchList = new List<Transform>();
    [SerializeField] private bool _beeping;
    [SerializeField] private bool _objectScanned = false;
    [SerializeField] private bool _objectCasted = false;
    [SerializeField] private AudioSource _beepAudio;

    [Header("PlayerCheckVariables")]
    [SerializeField] private float _minDistance;
    [SerializeField] private float _midDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _currentDistance;

    [SerializeField] private Transform _playerBody;
    [SerializeField] private LayerMask _scanMask;

    [SerializeField] private Transform _scannedObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ScanUpdate();
        BeepUpdate();
    }


    private void ScanUpdate()
    {
        if (!_objectScanned)
        {
            _objectCasted = false;
            
            int i = 0;
            float _lowestDistance = _minDistance;

            foreach(Transform _object in _searchList)
            {
                float _distance = Vector3.Distance(_playerBody.position, _object.position);
                if(_distance <= _minDistance)
                {
                    if(_distance <= _lowestDistance)
                    {
                        _scannedObject = _object;
                        _lowestDistance = _distance;
                    }
                   i++;
                }
            }

            if(i >= 1)
            {
                _objectScanned = true;
                
            }
            else
            {
                _objectScanned = false;
                _scannedObject = null;
            }
        }
        else
        {
            float _distance = Vector3.Distance(_playerBody.position, _scannedObject.position);
            if(_distance <= _minDistance)
            {
                Vector3 _direction = _playerBody.position - _scannedObject.position;
                RaycastHit hit;
                if (Physics.Raycast(_scannedObject.position, _direction, out hit, Mathf.Infinity, _scanMask))
                {
                    if(hit.transform.tag == "Player")
                    {
                        _objectCasted = true;
                    }
                    else
                    {
                        _objectCasted = false;
                    }
                }
                else
                {
                    _objectCasted = false;
                }
            }
            else
            {
                _objectScanned = false;
                _objectCasted = false;
            }
        }
        
    }

    private void BeepUpdate()
    {
        if (_objectCasted)
        {
            if (!_beepAudio.isPlaying)
            {
                _beepAudio.Play();
            }

            if (_beepAudio.loop == false)
            {
                _beepAudio.loop = true;
            }
        }
        else
        {
            if (_beepAudio.loop)
            {
                _beepAudio.loop = false;
            }
        }
    }


    




    
}
