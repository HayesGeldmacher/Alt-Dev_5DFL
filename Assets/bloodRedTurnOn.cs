using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodRedTurnOn : MonoBehaviour
{
    [SerializeField] private GameObject _bloodRed;
    
    // Start is called before the first frame update
   private void Start()
    {
        _bloodRed.SetActive(true);
    }

}
