using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTrigger : Interactable
{
    private bool _hasEncountered = false;
    bool _decreasing = false;
    bool _increasing = false;
    [SerializeField] private float _dayAmbience;
    [SerializeField] private float _nightAmbience;
    [SerializeField] private float _decreaseSpeed;
    private float _currentAmbience;
    private float _playerDistance;

    [SerializeField] private GameObject _waterLeak;
    [SerializeField] private GameObject _rockingChair;
    private bool _destroyedChair = false;


    private bool _canCollide = true;

    private void Start()
    {
        base.Start();
        _currentAmbience = _dayAmbience;
        
    }

    private void Update()
    {
        base.Update();

        if (_decreasing)
        {
        _currentAmbience -= 1 * _decreaseSpeed * Time.deltaTime;
       // Debug.Log(_currentAmbience);
        RenderSettings.ambientIntensity = _currentAmbience;

          if(_currentAmbience < 0.23f)
            {
                _decreasing = false;
                _increasing = false;
            }
        }
        else if (_increasing)
        {
            _currentAmbience += 1 * _decreaseSpeed * Time.deltaTime;
            //Debug.Log(_currentAmbience);
            RenderSettings.ambientIntensity = _currentAmbience;

            if (_currentAmbience > _dayAmbience)
            {
                _increasing = false;
                _increasing = false;
            }
        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (_canCollide)
        {
            if (!_destroyedChair)
            {
                _destroyedChair = true;
                Destroy(_rockingChair);
            }

            if (other.gameObject.tag == "Player")
            {
                _playerDistance = transform.position.y - other.transform.position.y;
                Debug.Log("PLAYERDIST:" + _playerDistance);
                _canCollide = false;
                StartCoroutine(ColliderWait());

                if (_playerDistance >= -0.8f)
                {
                    _decreasing = true;
                    _increasing = false;
                    _waterLeak.SetActive(true);

                }
                else
                {
                    _decreasing = false;
                    _increasing = true;
                    _waterLeak.SetActive(false);
                }

            }
        }
    }

    private IEnumerator ColliderWait()
    {
        yield return new WaitForSeconds(0.1f);
        _canCollide = true;
    }
}
