using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTVItems : ShootTrigger
{
    // Start is called before the first frame update
    [SerializeField] private nightManager _manager;
    [SerializeField] private bool _disappearItem = false;
    [SerializeField] private bool _appearItem = true;

    [SerializeField] private List<GameObject> _disappearItems = new List<GameObject>();
    [SerializeField] private List<GameObject> _appearItems = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        _manager.NextItem();
        _manager.CollectEvidence();

        if (_appearItem)
        {
            foreach(GameObject _item in _appearItems)
            {
                _item.SetActive(true);
            }
        }

        if (_disappearItem)
        {
            foreach(GameObject _item in _disappearItems)
            {
                _item.SetActive(false);
            }
        }


        Destroy(gameObject);
    }
}
