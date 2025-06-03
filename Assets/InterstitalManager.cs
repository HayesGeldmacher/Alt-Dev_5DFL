using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitalManager : MonoBehaviour
{

    public StartDataMosh _mosh;
    public CutsceneManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartCutscene()
    {
        //_mosh.CallGlitch();
        _manager.Begin();
        yield return new WaitForSeconds(0.1f);
    }
}
