using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeepingManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _searchList = new List<Transform>();
    [SerializeField] private bool _beeping;
    [SerializeField] private bool _objectScanned = false;
    [SerializeField] private bool _objectCasted = false;
    [SerializeField] private AudioSource _beepAudio1;
    [SerializeField] private AudioSource _beepAudio2;

    [Header("PlayerCheckVariables")]
    [SerializeField] private float _minDistance;
    [SerializeField] private float _midDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _currentDistance = 1000;

    [SerializeField] private Transform _playerBody;
    [SerializeField] private LayerMask _scanMask;

    [SerializeField] private Transform _scannedObject;

    [Header("Sound Variables")]
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;
    [SerializeField] private float _currentPitch;
    [SerializeField] private float _timeBeeping;

    [HideInInspector] private bool _hasCamera = true;
    [SerializeField] private bool _playSound = true;
    [SerializeField] private bool _playVisuals = true;
    [SerializeField] private Animator _heartAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (_hasCamera)
        {
            ScanUpdate();
            BeepUpdate();

        }

    }


    private void ScanUpdate()
    {

        if (!_objectScanned)
        {
            _objectCasted = false;

            int i = 0;
            float _lowestDistance = _maxDistance;

            foreach (Transform _object in _searchList)
            {
                if(_object == null)
                {
                    _searchList.RemoveAt(i);
                }
                else
                {

                    float _distance = Vector3.Distance(_playerBody.position, _object.position);
                    if (_distance <= _maxDistance)
                    {
                        if (_distance <= _lowestDistance)
                        {
                            _scannedObject = _object;
                            _lowestDistance = _distance;
                        }
                        i++;
                    }
                }
                
            }

            if (i >= 1)
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

            if(_scannedObject == null)
            {
                _objectScanned = false;
                _searchList.RemoveAll(item => item == null);
                return;
            }


            float _distance = Vector3.Distance(_playerBody.position, _scannedObject.position);

            if (_distance <= _maxDistance)
            {
                Vector3 _direction = _playerBody.position - _scannedObject.position;
                RaycastHit hit;
                if (Physics.Raycast(_scannedObject.position, _direction, out hit, Mathf.Infinity, _scanMask))
                {
                    if (hit.transform.tag == "Player")
                    {

                        if (!_objectCasted)
                        {
                            _heartAnim.SetBool("scanned", true);
                        }
                        
                        _objectCasted = true;
                        DistanceUpdate(_scannedObject);
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


    private void DistanceUpdate(Transform _castedObject)
    {
        _currentDistance = Vector3.Distance(_playerBody.position, _castedObject.position);
    }


    private void BeepUpdate()
    {
        if (_objectCasted)
        {

            //Get the percentage of the pitch min and max
            //first get the percentage of distance we are away!
            //so if max distance is 10 and min is 1, 9 is 10 percent of the way!
            //float _distancePercentage = (_currentDistance / _minDistance) ;
            float _distancePercentage = (_currentDistance - _maxDistance) / (_minDistance - _maxDistance);
            //float _distancePercentage = (_minDistance / _currentDistance);
            Debug.Log("DISTANCE: " + _distancePercentage);

            _currentPitch = (_distancePercentage);
            float _volume = Mathf.Clamp(_currentPitch, 0.1f, 0.4f);
            _currentPitch = Mathf.Clamp(_currentPitch, 0.3f, 0.7f);

           // _beepAudio1.pitch = _currentPitch;
            _beepAudio1.volume = _volume;

           // _beepAudio2.pitch = _currentPitch;
            _beepAudio2.volume = _volume;


            _heartAnim.SetBool("scanned", true);

            if(_distancePercentage > 0.30f)
            {
                if(_distancePercentage > 0.60f)
                {
                    
                    if(_distancePercentage > 0.8f)
                    {
                        _heartAnim.SetFloat("speed", 1.8f);
                    }
                    else
                    {
                    _heartAnim.SetFloat("speed", 1.5f);
                    }
                }
                else
                {
                    _heartAnim.SetFloat("speed", 1.2f);
                }
            }
            else
            {
                _heartAnim.SetFloat("speed", 1);
            }



        }
        else
        {
            _heartAnim.SetBool("scanned", false);
            _heartAnim.SetFloat("speed", 1);
        }








    }

}
