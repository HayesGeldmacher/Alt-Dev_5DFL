using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    [SerializeField] private int _picturesNeeded;
    public List<GameObject> _evidenceBits = new List<GameObject>();

    public void PictureTaken(GameObject evidence)
    {
        float i = 0;
        foreach(GameObject bit in _evidenceBits)
        {
            
            if bit.name == evidence.name)
            {
                _evidenceBits.Remove(i);
                _picturesNeeded -= 1;
                Debug.Log("Got a pic!");
                if(_picturesNeeded <= 0)
                {
                    CompletePictures();
                }
            }
            i++;
        }
    }

    private void CompletePictures()
    {
        Debug.Log("Got all of the pics!");
    }
}
