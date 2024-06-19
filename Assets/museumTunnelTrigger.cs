using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class museumTunnelTrigger : Interactable
{
    [SerializeField] private GameObject _tunnelDoor;
    [SerializeField] private bool _hasActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        _tunnelDoor.SetActive(false);
        _hasActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!_hasActivated)
            {
                _hasActivated = true;
                _tunnelDoor.SetActive(true);
                Debug.Log("collided!!! with Trigger!!");
            }
        }
    }
}
