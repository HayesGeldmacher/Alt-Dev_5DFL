using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerObject : MonoBehaviour
{

    private MeshRenderer _render;


    [Header("Flicker Fields")]
    public float _maxFlickerOnTime;
    public float _maxFlickerOffTime;

    public float _minFlickerOnTime;
    public float _minFlickerOffTime;

    public float _currentTime;
    public bool _goingDown;

    // Start is called before the first frame update
    void Start()
    {
        
        _render = transform.GetComponent<MeshRenderer>();
        _currentTime = _maxFlickerOnTime;
        _goingDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_goingDown)
        {
            _currentTime -= Time.deltaTime;
            if(_currentTime <= 0)
            {
                _render.enabled = false;
                _currentTime = Random.Range(_minFlickerOnTime, _maxFlickerOnTime);
                _goingDown = false;
            }
        }
        else
        {
            _currentTime -= Time.deltaTime;
            if(_currentTime <= 0)
            {
                _render.enabled = true;
                _currentTime = Random.Range(_minFlickerOffTime, _maxFlickerOffTime);
                _goingDown = true;
            }
        }
    }
}
