using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterApparaition : Interactable
{
    [SerializeField] private GameObject _monster;
    private bool _hasActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _monster.SetActive(false);
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
        _monster.SetActive(true);
        yield return new WaitForSeconds(1);
       // Destroy(_monster);
    }
}
