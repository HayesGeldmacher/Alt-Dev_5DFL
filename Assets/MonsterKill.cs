using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKill : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        KillPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillPlayer()
    {
        transform.position = _spawnPoint.position;
        Vector3 relativePos = _player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }
}
