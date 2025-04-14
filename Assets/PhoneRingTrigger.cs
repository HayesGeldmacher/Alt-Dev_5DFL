using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRingTrigger : Interactable
{
    private bool _hasEncountered = false;
    [SerializeField] PhoneDaytime _phoneDay;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_hasEncountered)
        {
            if (other.gameObject.tag == "Player")
            {
                _hasEncountered = true;

                if (_phoneDay != null)
                {
                    _phoneDay.StartRing();
                }

                StartCoroutine(DialogueStart());
            }
        }
    }
    public void CallDialogueStart()
    {
        StartCoroutine(DialogueStart());
    }

    private IEnumerator DialogueStart()
    {
        yield return new WaitForSeconds(1f);
        base.Interact();
    }
}
