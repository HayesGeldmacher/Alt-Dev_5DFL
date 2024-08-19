using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlagNight : MonoBehaviour
{
    [SerializeField] private float _flagsCleared = 0;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _flagMask;
    [SerializeField] private nightManager _nightmanage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit _hitInfo = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out _hitInfo, _range, _flagMask))
        {
            if (_hitInfo.transform.tag == "flag")
            {
                _flagsCleared++;
                Destroy(_hitInfo.transform.gameObject);

                if(_flagsCleared >= 2)
                {
                    _nightmanage.SpawnFirst();
                }
            }
        }
    }
}
