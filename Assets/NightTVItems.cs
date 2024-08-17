using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTVItems : ShootTrigger
{
    // Start is called before the first frame update
    [SerializeField] private nightManager _manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        _manager.NextItem();
        Destroy(gameObject);
    }
}
