using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareTriggerKitchen : MonoBehaviour
{

    //set it so you cannot do the same room twice!


    [SerializeField] private nightManager _nightManage;
    [SerializeField] private GameObject _currentRoomLight;
 


    [SerializeField] private bool _partnerTriggered = false;
    public bool _enterTrigger = false;

    [SerializeField] private NightmareTriggerKitchen _roomPartner;
    [SerializeField]private bool _canTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        _nightManage = GameObject.Find("GameManager").transform.GetComponent<nightManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerExit(Collider other)
    {
        if (_canTrigger)
        {
            _canTrigger = false;
            if (other.tag == "Player")
            {
                if (_enterTrigger)
                {
                    EnterRoom();
                }
                else
                {
                    ExitRoom();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _canTrigger = true;
        }
    }

    private void OldOnTriggerExit(Collider other)
    {

        if (_canTrigger)
        {
        
            Debug.Log("TRIGGERED!");
            _canTrigger = false;
            StartCoroutine(TriggerWait());


            if (other.tag == "Player")
            {
               
                    if (_partnerTriggered)
                    {
                        _partnerTriggered = false;
                        EnterRoom();
                    }
                    else
                    {
                        _roomPartner.ExitedOtherRoom();
                        ExitRoom();
                    }
               
            }

        }

    }

    private void ExitRoom()
    {
        _currentRoomLight.SetActive(false);
    }

    private void EnterRoom()
    {
        _currentRoomLight.SetActive(true);
        _partnerTriggered = false;
        _nightManage.AddRoom(this);
        Debug.Log("ENTERED!");

    }

    private void SpawnExit()
    {
        Debug.Log("ExitedRoom");
    }

    public void ExitedOtherRoom()
    {
        _partnerTriggered = true;
    }

    private IEnumerator TriggerWait()
    {
        yield return new WaitForSeconds(0.3f);
        _canTrigger = true;
    }

}
