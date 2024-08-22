using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareTriggerKitchen : MonoBehaviour
{

    [SerializeField] private GameObject _currentRoomLight;
    [SerializeField] private int _neededRooms;
    [SerializeField] private int _currentRooms;


    [SerializeField] private bool _partnerTriggered = false;

    //keeping track of the room before and after us

    [SerializeField] private NightmareTriggerKitchen _roomPartner;
    private bool _canTrigger = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {

        if (_canTrigger)
        {
        
            Debug.Log("TRIGGERED!");
            _canTrigger = false;
            StartCoroutine(TriggerWait());


            if (other.tag == "Player")
            {
                if(_currentRooms > _neededRooms)
                {
                    SpawnExit();
                }
                else
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

    }

    private void ExitRoom()
    {
        _currentRoomLight.SetActive(false);
    }

    private void EnterRoom()
    {
        _currentRoomLight.SetActive(true);
        _currentRooms++;
        _partnerTriggered = false;

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
