using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMonsterTrigger : Interactable
{
    [SerializeField] private Phone _phone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            _phone.CallSpawnBathroom();
        Debug.Log("FUUUCKCKCCK~!");
            Destroy(gameObject);
        }
    }
}
