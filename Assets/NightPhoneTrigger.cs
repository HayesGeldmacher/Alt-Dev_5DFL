using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightPhoneTrigger : Interactable
{
    private bool _hasEncountered = false;

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
