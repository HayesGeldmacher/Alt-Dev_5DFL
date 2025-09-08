using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDreamManager : MonoBehaviour
{
    [Header("Shot Fields")]
    public StaticDreamPosition[] _shotPositions;
    public int _currentShot;
    public int _maxShot;
    public StartDataMosh _dataMosh;


    public Transform _player;


    [Header("CountDown Fields")]
    public float _clickCountDown = 2;
    private float _currentClickDown = 1;
    public bool _canClick = false;
    

    //The below region just creates a reference of this specific controller that we can call from other scripts quickly
    #region Singleton

    public static StaticDreamManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of playercontroller present!! NOT GOOD!");
            return;
        }

        instance = this;
    }

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        CallNextShot();
        _currentClickDown = _clickCountDown;
    }

    // Update is called once per frame
    void Update()
    {


        if (_canClick)
        {
            if (Input.GetButtonDown("Interact"))
            {
                _canClick = false;
                _currentClickDown = _clickCountDown;
                CallNextShot();
            }

        }
        else
        {
            _currentClickDown -= Time.deltaTime;
            if(_currentClickDown <= 0)
            {
                _canClick = true;
            }
        }

    }

    private void CallNextShot()
    {
        _shotPositions[_currentShot].CallPositionShot();
        _currentShot++;
    }

    public void NextShot(Transform nextPosition)
    {
        _dataMosh.CallGlitch();
        _player.localPosition = nextPosition.localPosition;
        _player.localRotation = nextPosition.localRotation;
    }

}
