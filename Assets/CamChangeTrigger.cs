using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamChangeTrigger : MonoBehaviour
{


    [SerializeField] protected private CamChangePositions _camChange;
    [SerializeField] protected private int _camNum;
    protected bool _canEnter = true;
    [SerializeField] private GameObject[] _disappearObjects;
    [SerializeField] private GameObject[] _appearObjects;

    [Header("TeleportVariables")]
    [SerializeField] private protected bool _teleportPlayer;
    [SerializeField] private protected Transform _player;
    [SerializeField] private protected Transform _spawnPos;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_canEnter && _camNum != _camChange._currentCam)
        {
            if(other.tag == "Player")
            {
               _camChange.ChangePos(_camNum); 
               _canEnter = false;
                CallGeneric();
            }
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            _canEnter = true;
        }
    }

    public virtual void CallGeneric()
    {
        if(_appearObjects.Length > 0)
        {
            foreach(GameObject item in _appearObjects)
            {
               if(item != null)
                {
                item.SetActive(true);
                }
            }
        }

        if(_disappearObjects.Length > 0)
        {
            foreach(GameObject item in _disappearObjects)
            {
                if (item != null)
                {
                 item.SetActive(false);
                }
            }
        }

        if (_teleportPlayer && _player != null)
        {
            CharacterController _controller = _player.GetComponent<CharacterController>();
            _controller.enabled = false;
            _player.position = _spawnPos.position;
            _controller.enabled = true;
        }
    }

    private void TeleportPlayer()
    {

    }
}
