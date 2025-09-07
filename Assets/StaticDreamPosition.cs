using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDreamPosition : MonoBehaviour
{

    public GameObject[] appearObjects;
    public GameObject[] disappearObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPositionShot()
    {
        if(disappearObjects.Length > 0)
        {
            DisappearObjects();
        }
        if(appearObjects.Length > 0)
        {
            AppearObjects();
        }
        StaticDreamManager.instance.NextShot(transform);
    }

    private void DisappearObjects()
    {
        foreach(GameObject thing in disappearObjects)
        {
            thing.SetActive(false);
        }
    }

    private void AppearObjects()
    {
        foreach(GameObject thing in appearObjects)
        {
            thing.SetActive(true);
        }
    }
}
