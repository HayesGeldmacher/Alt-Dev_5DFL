using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handcuffs : ShootTrigger
{

    [SerializeField] private armCuffs _cuffs;
    [SerializeField] private EvidenceManager _evidenceManager;


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
        _cuffs.CallArms();
        _evidenceManager.StrikeOffItem(gameObject);
        _evidenceManager.PictureTaken(gameObject);
    }
}
