using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampFloating : MonoBehaviour
{
    [SerializeField] private float _floatSpeed;
    [SerializeField] private Transform _lamp;
    [SerializeField] private Transform[] _lampPositions;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private int _currentTarget = 0;
    [SerializeField] private bool _moving;
    [SerializeField] private Animator _lampAnim;
    [SerializeField] private Transform _player;
    [SerializeField] private float _playerDistance;
    [SerializeField] private bool _finished = false;
    [SerializeField] private AudioSource _giggle;

    private Vector3 targetDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        _targetPosition = _lampPositions[0];
        float distance = Vector3.Distance(_targetPosition.position, _lamp.transform.position);
        targetDirection = (_targetPosition.position - _lamp.position) / distance;
        foreach (Transform thing in _lampPositions)
        {
            thing.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!_finished)
        {
            if (_moving)
            {
                MoveUpdate();
            }
            else
            {
                CheckForPlayer();
            }

        }
        

    }


    private void CheckForPlayer()
    {
        RaycastHit hit;
        Vector3 directiton = _player.position - _lamp.position;
        if(Physics.Raycast(_lamp.transform.position, directiton, out hit, _playerDistance))
        {
            _moving = true;
            PlayGiggle();
        }
    }

    private void MoveUpdate()
    {
        bool stopMoving = false;
        
        if (_targetPosition.gameObject.tag == "stopLamp")
        {
            stopMoving = true;
        }


        Vector3 newPosition;
        float distance = Vector3.Distance(_targetPosition.position, _lamp.transform.position);
        if(stopMoving && distance < 2)
        {
            newPosition = _lamp.position + (targetDirection * (_floatSpeed/2) * Time.deltaTime);
        }
        else
        {
            newPosition = _lamp.position + (targetDirection * _floatSpeed * Time.deltaTime);
        }

        _lamp.position = newPosition;

        if(distance <= 1f)
        {
            if(stopMoving)
            {
                StopMoving();
            }
            IterateTarget();
        }
    }

    private void IterateTarget()
    {
        _currentTarget++;
        if(_currentTarget > _lampPositions.Length)
        {
            _finished = true;
        }
        else
        {
         _targetPosition = _lampPositions[_currentTarget];
         float distance = Vector3.Distance(_targetPosition.position, _lamp.transform.position);
         targetDirection = (_targetPosition.position - _lamp.position) / distance;
        }
        
    }

    private void StopMoving()
    {
        _moving = false;
    }

    private void StartMoving()
    {
        _moving = true;
    }

    private void PlayGiggle()
    {
        _giggle.pitch = Random.Range(0.8f, 1.2f);
        _giggle.Play();
    }
}
