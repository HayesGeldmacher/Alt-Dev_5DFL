using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEvidence :  ShootTrigger
{

    [SerializeField] private ScreenshotHandler _handler;

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
        _handler.CallEvidenceDing();
        Destroy(gameObject);
    }
}
