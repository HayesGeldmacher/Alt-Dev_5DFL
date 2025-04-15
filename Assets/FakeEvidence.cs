using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeEvidence :  ShootTrigger
{

    [SerializeField] private ScreenshotHandler _handler;
    private bool _triggered = false;

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
        if (!_triggered)
        {
            _triggered = true;
            _handler.CallEvidenceDing();
            StartCoroutine(DestroyVase());
        }

    }

    private IEnumerator DestroyVase()
    {
       
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        _triggered = false;
    }
}
