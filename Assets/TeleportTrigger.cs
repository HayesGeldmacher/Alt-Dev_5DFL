using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
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

    public bool _rotatePlayer = false;
    [SerializeField] private Transform _targetRot;
    public bool _shouldChangeFlash = true;
    public float yRotationValue;
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


    public void CallTeleport()
    {
        TeleportPlayer();
    }


    private void TeleportPlayer()
    {
        if (_shouldChangeFlash)
        {

            bool _changeFlash = _camZoom.CheckFlash();
            if (_changeFlash)
            {
            _camZoom.TurnOffFlash();
                StartCoroutine(FlashBackOn());
            }


        }
        if(_rotatePlayer && _targetRot != null)
        {

            Vector3 relativePos = _targetRot.position - _playerParent.transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            _playerParent.transform.rotation = Quaternion.Euler(_playerParent.transform.localRotation.x, yRotationValue, _playerParent.transform.localRotation.z);
        }

        if (_appear)
        {
            AppearObjects();
        }

        if (_disappear)
        {
            DisappearObjects();
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
        _camZoom.TurnOnFlash();
    }
}
