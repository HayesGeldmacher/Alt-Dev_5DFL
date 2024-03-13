using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoaming : MonoBehaviour
{

    [SerializeField] private float _listenRange;
    [SerializeField] private float _killRange;
    [SerializeField] private float _killCountDown;
    public float _currentKillCountDown;
    [SerializeField] private Transform _player;
    public bool _listening = false;
    public bool _inSight = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentKillCountDown = _killCountDown;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if player is in range, if so it starts listening
        float range = Vector3.Distance(transform.position, _player.position);
        Vector3 direction = _player.position - transform.position;
        if(range <= _listenRange)
        {
            _listening = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, _listenRange)){
                if(hit.transform.tag == "Player")
                {
                    if (Vector3.Dot(transform.forward, _player.position - transform.position) > 0)
                    {
                        _inSight = true;

                    }
                    else
                    {
                        _inSight = false;
                    }

                }
                else
                {
                    _inSight = false;
                }
            }
            else
            {
                _inSight=false;
            }

            if(range <= _killRange || _inSight)
            {
                _currentKillCountDown -= Time.deltaTime;
                if(_currentKillCountDown <= 0)
                {
                    GameManager.instance.SpawnKillMonster();
                    Destroy(gameObject);
                }
            }
            else
            {
                _currentKillCountDown = _killCountDown;
            }
        }
        else
        {
            _listening = false;
            _currentKillCountDown = _killCountDown;
        }
    }

   
}
