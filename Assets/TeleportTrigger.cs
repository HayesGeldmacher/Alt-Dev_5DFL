using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{


    [SerializeField] private Transform _newSpawnPos;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private CharacterController _char;
    [SerializeField] private PlayerController _controller;

    [SerializeField] private bool _disappear;
    [SerializeField] private bool _appear;

    [SerializeField] private List<GameObject> _disappearObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> _appearObjects = new List<GameObject>();


    [SerializeField] private CameraZoom _camZoom;
    private bool _flashActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TeleportPlayer();

            if (_appear)
            {
                AppearObjects();
            }

            if (_disappear)
            {
                DisappearObjects();
            }
        }
    }



    private void AppearObjects()
    {
        foreach(GameObject _item in _appearObjects)
        {
            _item.SetActive(true);
        }
    }

    private void DisappearObjects()
    {
        foreach (GameObject _item in _disappearObjects)
        {
            _item.SetActive(false);
        }
    }


    private void TeleportPlayer()
    {

        bool _changeFlash = _camZoom.CheckFlash();
        if (_changeFlash)
        {
        _camZoom.TurnOffFlash();
            StartCoroutine(FlashBackOn());
        }

        _playerParent.SetActive(false);
        //_controller._frozen = true;
        //_char.enabled = false;
        _playerParent.transform.position = _newSpawnPos.position;
        //Debug.Break();
        _playerParent.SetActive(true);
        _char.enabled = true;
        _controller._frozen = false;

    }

    private IEnumerator FlashBackOn()
    {
        yield return new WaitForSeconds(1f);
        _camZoom.TurnOffFlash();
    }
}
