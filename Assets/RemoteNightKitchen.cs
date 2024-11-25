using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteNightKitchen : ShootTrigger
{

    [SerializeField] private nightManager _nightManage;
    [SerializeField] private GameObject _parentObject;
    [SerializeField] private AudioClip _shushSound;

    public override void Interact()
    {
        _nightManage.CallExitKitchen();
        _nightManage.CallDataMosh();

        if(_shushSound != null)
        {
            _nightManage.PlaySound(_shushSound);
        }
        Destroy(_parentObject);
    }
}
