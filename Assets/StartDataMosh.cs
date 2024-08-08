using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDataMosh : MonoBehaviour
{
    [SerializeField] private Datamosh _data;
    
    // Start is called before the first frame update
    void Start()
    {
       // _data = transform.GetComponent<Datamosh>();
        _data.Glitch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
