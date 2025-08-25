using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditCameraAnimCaller : MonoBehaviour
{

    [SerializeField] private CreditsManager _creditsManager;
    
    public void CallCredits()
    {
        _creditsManager.BeginCreditsCountdown();
    }
}
