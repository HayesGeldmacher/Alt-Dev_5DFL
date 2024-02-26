using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{

    [SerializeField] private EvidenceManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
       if(_manager._hasEvidence && _manager._hasFoundPhone)
        {
            Debug.Log("Finished sequence!");
        }
        else
        {
            Debug.Log("Theres more to do");
        }
       

    }
}
