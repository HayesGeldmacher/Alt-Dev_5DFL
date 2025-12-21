using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NightTVItems : ShootTrigger
{
    // Start is called before the first frame update
    [SerializeField] private nightManager _manager;
    [SerializeField] private bool _disappearItem = false;
    [SerializeField] private bool _appearItem = true;

    [SerializeField] private List<GameObject> _disappearItems = new List<GameObject>();
    [SerializeField] private List<GameObject> _appearItems = new List<GameObject>();

    public bool _teleport = false;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private Transform _newSpawnPosition;
    [SerializeField] private bool _rotateOnTeleport = false;
    public float rotationYValue;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (_teleport)
        {
            if(_playerBody != null)
            {
                if(_charController != null)
                {
                    _charController.enabled = false;
                    _playerBody.position = _newSpawnPosition.position;
                    if (_rotateOnTeleport)
                    {
                        

                        // the second argument, upwards, defaults to Vector3.up
                       // Quaternion rotation = Quaternion.LookRotation(_newSpawnPosition.rotation., Vector3.up);

                        _playerBody.rotation = Quaternion.Euler(_playerBody.rotation.x, rotationYValue, _playerBody.rotation.z);
                    }
                    _charController.enabled = true;
                }
            }
        }

        if (_appearItem)
        {
            foreach(GameObject _item in _appearItems)
            {
               if(_item != null )
                {
                _item.SetActive(true);
                }
            }
        }

        if (_disappearItem)
        {
            foreach(GameObject _item in _disappearItems)
            {
               if(_item != null)
                {
                _item.SetActive(false);

                }
            }
        }

        _manager.NextItem();
        _manager.CollectEvidence();
    }
}
