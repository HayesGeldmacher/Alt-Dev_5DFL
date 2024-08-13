using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionTrigger : MonoBehaviour
{
    [SerializeField] private bool _appears = true;
    [SerializeField] private bool _disappears = true;
    [SerializeField] private bool _destroys = true;

    [SerializeField] private List<GameObject> _appearObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _disappearObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _destroyObjects = new List<GameObject>();

    private BoxCollider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Apparatus();
            _collider.enabled = false;
        }
    }

    private void Apparatus()
    {
        if (_appears)
        {
            foreach(GameObject _object in _appearObjects)
            {
                _object.SetActive(true);
            }
        }

        if (_disappears)
        {
            foreach(GameObject _object in _disappearObjects)
            {
                _object.SetActive(false);
            }
        }

        if(_destroys)
        {
            foreach (GameObject _object in _destroyObjects)
            {
                Destroy(_object);
            }
        }
    }
}
