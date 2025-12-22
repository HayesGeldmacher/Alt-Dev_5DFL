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
    [SerializeField] private FlashilghtRot _flash;

    [SerializeField] private Transform _playerBody;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private Transform _newSpawnPosition;


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
        if (_flash._active)
        {
           _flash.ChangeFlashStatus();
        }
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject _obj in _disappearItems)
        {
            _obj.SetActive(false);
        }
        foreach (GameObject _obj in _appearItems)
        {
            _obj.SetActive(true);
        }

        _charController.enabled = false;
        _playerBody.position = _newSpawnPosition.position;
        _charController.enabled = true;

        transform.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        _spotLightSound.Play();
        _light1.SetTrigger("appear");
        Destroy(gameObject);

    }
}
