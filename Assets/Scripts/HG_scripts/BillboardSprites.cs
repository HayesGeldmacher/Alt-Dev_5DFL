using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprites : MonoBehaviour
{

    [SerializeField] private Transform _sprite;
    private Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        if(_sprite == null)
        {
            _sprite = transform;
        }

        _player = PlayerController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
