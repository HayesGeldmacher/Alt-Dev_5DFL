using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private float _speed = 1;
    public float _currentTime;
    public float _seconds;
    public float _minutes;
    public float _hours;

    // Start is called before the first frame update
    void Start()
    {
        _seconds = 0;
        _minutes = 0;
        _hours = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _seconds += Time.deltaTime * 0.01f * _speed;
        if(_seconds > 0.59f)
        {
            _minutes += 1;
            _seconds = 0;
        }
        if(_minutes > 59)
        {
            _hours += 1;
            _minutes = 0;
            _seconds = 0;
        }


        string _totalTime = _hours.ToString("#0") + ":" + _minutes.ToString("00") + ":" +  _seconds.ToString(".00");
        // string _totalTime = _hours.ToString("#0") + ":" + _minutes.ToString("00") + ":" + _seconds.ToString("00.00");
        _timeText.text = _totalTime;
        
    }
}
