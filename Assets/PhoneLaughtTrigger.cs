using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PhoneLaughtTrigger : MonoBehaviour
{
[SerializeField] private AudioSource _creepyLaughing;
[SerializeField] private AudioSource _phoneRinging;


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_creepyLaughing.isPlaying)
            {
                _creepyLaughing.Play();
            }
            if (!_phoneRinging.isPlaying)
            {
                _phoneRinging.Play();
            }
            transform.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
