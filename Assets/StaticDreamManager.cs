using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDreamManager : MonoBehaviour
{
    [Header("Shot Fields")]
    public StaticDreamPosition[] _shotPositions;
    public int _currentShot;
    public int _maxShot;
    public bool _canMove = true;

    public Transform _player;

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
        _shotPositions[0].CallPositionShot();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (_canMove)
                {
                CallNextShot();
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
        _player.localPosition = nextPosition.localPosition;
        _player.localRotation = nextPosition.localRotation;
    }
}
