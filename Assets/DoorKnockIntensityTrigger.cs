using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnockIntensityTrigger : MonoBehaviour
{
    [Header("Collision Fields")]
    [SerializeField] private BoxCollider collider;
    
    [Header("Door Knock Fields")]
    [SerializeField] private float newMinIntensity;
    [SerializeField] private float newMaxIntensity;
    [SerializeField] private DoorKnocking knocking;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            knocking.SetIntensity(newMinIntensity, newMaxIntensity);
            collider.enabled = false;
        }
    }
}
