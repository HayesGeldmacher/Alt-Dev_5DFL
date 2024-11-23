using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightEvidenceManager : MonoBehaviour
{
    [SerializeField] private int _evidenceCount;
    [SerializeField] private int _totalEvidenceNeeded = 4;
    [SerializeField] private GameObject _newPhone;
    [SerializeField] private GameObject _oldPhone;
    private bool _completedEvidence = false;
 
    private void Start()
    {
        _evidenceCount = 0;
    }

    public void AddEvidence()
    {
        _evidenceCount++;
        if(_evidenceCount >= _totalEvidenceNeeded)
        {
            if (!_completedEvidence)
            {
                _completedEvidence = true;
                EvidenceComplete();
            }
        }
    }

    private void EvidenceComplete()
    {
        Debug.Log("newPhoneDONE!");
        _oldPhone.SetActive(false);
        _newPhone.SetActive(true);
        



    }
}
