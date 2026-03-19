using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenValveInteract : Interactable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _screechSound;

    [SerializeField] private GameObject[] _disappearObjects;
    [SerializeField] private TeleportTrigger _teleport;

    [SerializeField] FlashilghtRot _flashilghtRot;

    [SerializeField] private nightManager _nightManager;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        _anim.SetTrigger("turn");
        _screechSound.Play();
        if(_flashilghtRot._active)
        {
            _flashilghtRot.ChangeFlashStatus();
        }
        StartCoroutine(DisableNightmareKitchen());
       


    }

    private IEnumerator DisableNightmareKitchen()
    {
        yield return new WaitForSeconds(1);
        //_nightManager.CallDataMosh();
        if (_disappearObjects.Length > 0)
        {
            foreach (var obj in _disappearObjects)
            {
                obj.gameObject.SetActive(false);

            }
        }
        yield return new WaitForSeconds(2);
        _teleport.CallTeleport();
      //  _nightManager.CallExitKitchen();
    }
}
