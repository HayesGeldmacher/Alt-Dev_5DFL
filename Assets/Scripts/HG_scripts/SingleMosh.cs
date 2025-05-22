using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMosh : MonoBehaviour
{
    [SerializeField] private Datamosh _data;

    public static SingleMosh instance;

    void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // _data = transform.GetComponent<Datamosh>();
        _data.Glitch();
    }

    public void CallGlitch()
    {
        _data.Glitch();
    }
}
