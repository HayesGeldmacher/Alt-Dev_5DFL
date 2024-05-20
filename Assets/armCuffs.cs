using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armCuffs : MonoBehaviour
{
    [SerializeField] private Animator _arm1;
    [SerializeField] private Animator _arm2;

    [SerializeField] private AudioSource _audio;
    
    public void CallArms()
    {
        StartCoroutine(Arms());
    }
   private IEnumerator Arms()
    {
        yield return new WaitForSeconds(0.2f);
        _arm1.SetTrigger("away");
        _arm2.SetTrigger("away");
        _audio.Play();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
