using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlspaceNightTrigger : MonoBehaviour
{

    [SerializeField] private GameObject _crawlSpaceBlocker;
    [SerializeField] private AudioSource _thumpSound;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _crawlSpaceBlocker.SetActive(false);
            _thumpSound.Play();

        }
    }


}
