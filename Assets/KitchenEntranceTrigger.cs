using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenEntranceTrigger : MonoBehaviour
{
    private bool _canTrigger = true;
    [SerializeField] private List<GameObject> _disappearItems = new List<GameObject>();
    [SerializeField] private List<GameObject> _appearItems = new List<GameObject>();

    [SerializeField] private Animator _light1;

    [SerializeField] private AudioSource _spotLightSound;

    private void Start()
    {
        _light1.SetTrigger("appear");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if (_canTrigger)
            {
                _canTrigger = false;
                StartCoroutine(TransitionLevel());
            
            }
        }
    }

    private IEnumerator TransitionLevel()
    {
        _light1.SetTrigger("fade");
        _spotLightSound.Play();
        yield return new WaitForSeconds(1f);
        foreach (GameObject _obj in _disappearItems)
        {
            _obj.SetActive(false);
        }
        foreach (GameObject _obj in _appearItems)
        {
            _obj.SetActive(true);
        }


        _spotLightSound.Play();
        transform.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}
