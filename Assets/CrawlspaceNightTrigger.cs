using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlspaceNightTrigger : MonoBehaviour
{

    [SerializeField] private GameObject _crawlSpaceBlocker;
    [SerializeField] private AudioSource _thumpSound;
    [SerializeField] private GameObject _crawlSpaceDadHusk;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _crawlSpaceBlocker.SetActive(false);
            _thumpSound.Play();
            
            if(_crawlSpaceDadHusk != null)
            {
                _crawlSpaceDadHusk.SetActive(false);
            }


        }
    }


}
