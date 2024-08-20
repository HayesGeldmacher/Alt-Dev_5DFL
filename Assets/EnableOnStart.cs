using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnStart : MonoBehaviour
{

    [SerializeField] private GameObject _object;
    

    // Start is called before the first frame update
    void Start()
    {
        _object.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
