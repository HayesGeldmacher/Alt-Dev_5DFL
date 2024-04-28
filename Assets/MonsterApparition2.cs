using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApparition2 : MonoBehaviour
{
    [SerializeField] private GameObject _trigger;
    [SerializeField] private AudioSource _patter;
    private bool _hasActivated = false;

    // Start is called before the first frame update
    void Start()
    {
       _trigger.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            if (!_hasActivated)
            {
                _hasActivated = true;
                StartCoroutine(Activate());
            }
        }
    }


    private IEnumerator Activate()
    {
        _trigger.SetActive(true);
        _patter.Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
