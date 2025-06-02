using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstToThirdTransition : MonoBehaviour
{

    [SerializeField] private bool _entered = false;
    [SerializeField] private GameObject _playerFirstPerson;
    [SerializeField] private GameObject _playerThirdPerson;
    [SerializeField] private GameObject _playerFirstHUD;
    [SerializeField] private GameObject _playerThirdHUD;
    [SerializeField] private StartDataMosh _mosh;
    [SerializeField] private GameObject _SunBeams;

    [SerializeField] private float _lightValue;
    [SerializeField] private float _fogDensity;
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("COLLIDEDWITHTRIGGA");
            if (!_entered)
            {
              EnterFirstPerson();
                _entered = true;
            }
        }


    }

    private void EnterFirstPerson()
    {
        _playerFirstPerson.SetActive(true);
        _playerFirstHUD.SetActive(true);
        _playerThirdHUD.SetActive(false);
        _playerThirdPerson.SetActive(false);
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _mosh.CallGlitch();

        RenderSettings.ambientIntensity = _lightValue;
        RenderSettings.fogDensity = _fogDensity;

        _SunBeams.SetActive(false);
    }

}
