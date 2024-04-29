using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelMonster : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _playerRange;
    private bool _inSight = false;
    private bool _inRange = false;
    private bool _startedDisappear = false;
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _chimes;
     public float _range;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        _range = Vector3.Distance(_player.position, transform.position);

        if(Vector3.Distance(_player.position, transform.position) <= _playerRange)
        {
            _inRange = true;
        }
        else
        {
            _inRange = false;
        }

        if(_inRange && !_startedDisappear)
        {
            _startedDisappear = true;
            StartCoroutine(StartDisappear());
        }

    }
    
    private IEnumerator StartDisappear()
    {
        Debug.Log("Started Disappearing!");
        _anim.SetTrigger("fade");
        _chimes.Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
