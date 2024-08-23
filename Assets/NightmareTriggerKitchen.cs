using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmareTriggerKitchen : MonoBehaviour
{

    [SerializeField] private newKitchenCollider _colParent;
    [SerializeField] private bool _lightStartsOn = false;

    private bool _canTrigger = true;
    [SerializeField] private Animator _light;


    private void Start()
    {
        if (_lightStartsOn)
        {
            _light.SetTrigger("appear");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_canTrigger)
        {
            _canTrigger = false;
            StartCoroutine(TriggerWait());
            if (other.tag == "Player")
            {
                _colParent.SetChildFlag(this);

            }
        }
    }

    private IEnumerator TriggerWait()
    {
        
        yield return new WaitForSeconds(0.2f);
        _canTrigger = true;
    }


    public void StartFade()
    {
        _light.SetTrigger("fade");
    }

    public void StartAppear()
    {
        _light.SetTrigger("appear");
    }
}
