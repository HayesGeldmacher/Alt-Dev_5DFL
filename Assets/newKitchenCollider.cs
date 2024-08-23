using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newKitchenCollider : Interactable
{

    [SerializeField] private NightmareTriggerKitchen _colFlag1;
    [SerializeField] private NightmareTriggerKitchen _colFlag2;
    [SerializeField] private NightmareTriggerKitchen _currentFlag;

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

        if(other.tag == "Player")
        {

            if (_canTrigger)
            {
                _canTrigger = false;
                StartCoroutine(TriggerWait());

                if(_colFlag1 == _currentFlag)
                {
                    _colFlag1.StartFade();
                    _colFlag2.StartAppear();
                    Debug.Log("StartFade1");
                }
                else
                {
                    _colFlag1.StartAppear();
                    _colFlag2.StartFade();
                }

            }
            
        }
    }


    public void SetChildFlag(NightmareTriggerKitchen _passedFlag)
    {
        _currentFlag = _passedFlag;
    }

    private IEnumerator TriggerWait()
    {
        yield return new WaitForSeconds(0.1f);
        _canTrigger = true;
    }

}
