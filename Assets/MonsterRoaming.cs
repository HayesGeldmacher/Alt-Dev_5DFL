using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoaming : MonoBehaviour
{

    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillState()
    {
        transform.position = _playerSpawn.position;
       //set anim here once we have the animation
    }
}
