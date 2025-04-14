using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlspaceNightTrigger : MonoBehaviour
{
    private bool _hasTriggered = false;
    [SerializeField] private GameObject _crawlSpaceBlocker;
    [SerializeField] private AudioSource _thumpSound;
    [SerializeField] private GameObject _crawlSpaceDadHusk;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!_hasTriggered)
            {
                _hasTriggered = true;
                _crawlSpaceBlocker.SetActive(false);
                _thumpSound.Play();
            
                if(_crawlSpaceDadHusk != null)
                {
                    _crawlSpaceDadHusk.SetActive(false);
                }
            }
        }
    }


}
