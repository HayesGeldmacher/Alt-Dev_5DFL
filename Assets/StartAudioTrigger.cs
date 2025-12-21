using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    private bool hasTriggered = false;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!hasTriggered)
            {
               
                hasTriggered = true;
                TriggerAudio();
            }
        }
    }

    private void TriggerAudio()
    {
        if (_audio != null)
        {
            _audio.Play();

        }
    }
}
