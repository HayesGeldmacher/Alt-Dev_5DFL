using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Animator _camAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }


    private IEnumerator StartScene()
    {
        //tv turns on
        //then camera slowly sways away
        //then the "ritual static" logo comes up
        //then credits play

        yield return new WaitForSeconds(20f);
        _camAnim.SetTrigger("sway");
        
    }
}
