using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteNightKitchen : ShootTrigger
{

    [SerializeField] private nightManager _nightManage;
    [SerializeField] private GameObject _parentObject;
    
    public override void Interact()
    {

        _nightManage.CallExitKitchen();
        _nightManage.CallDataMosh();
        Destroy(_parentObject);
    }
}
