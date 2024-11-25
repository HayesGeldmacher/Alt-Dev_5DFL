using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlDadTrigger : MonoBehaviour
{

    [SerializeField] private Animator _crawlDad;
    private bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           if(!_hasTriggered)
            {
                _hasTriggered = true;
                _crawlDad.SetTrigger("disappear");

            }

        }
    }

}
