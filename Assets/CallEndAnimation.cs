using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CallEndAnimation : MonoBehaviour
{
    [SerializeField] private TripodInteract _tripod;
    [SerializeField] private DollTalk _dollTalk;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndAnimation()
    {
        _tripod.CallAnimEnd();
    }

    public void EndDollAnimation()
    {
        _dollTalk.CallAnimEnd();
    }

    public void CallDestroyDoll()
    {
        _dollTalk.CallDestroyObjects();
    }

}
