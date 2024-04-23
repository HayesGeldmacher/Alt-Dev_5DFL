using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApparaition : Interactable
{
    [SerializeField] private GameObject _monster;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _monster.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            _monster.SetActive(true);
        }
    }
}
