using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    private float _currentTime;
    private float _seconds;
    private float _minutes;
    private float _hours;

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
        _seconds += Time.deltaTime * 0.01f;
        if(_seconds > 59)
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

        string _totalTime = _hours.ToString("#0") + ":" + _minutes.ToString("00") + ":" + _seconds.ToString("00.00");
       _timeText.text = _totalTime;
        
    }
}
