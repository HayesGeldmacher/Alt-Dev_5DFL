using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionTriggerFace : MonoBehaviour
{
    [SerializeField] ScreenshotHandler _handler;
    [SerializeField] private BoxCollider _bc;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _bc.enabled = false;
            EnterFace();
        }
    }

    private void EnterFace()
    {
        _handler.CallSetMonster();
    }
}
