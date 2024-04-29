using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApparaition : Interactable
{
    [SerializeField] private GameObject _monster;
    private bool _hasActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _monster.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
        Debug.Log("collided!!! with Trigger!!");
            if (!_hasActivated)
            {
                _hasActivated = true;
                _monster.SetActive(true);
            }
        }
    }

}
