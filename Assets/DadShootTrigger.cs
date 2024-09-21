using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadShootTrigger : ShootTrigger
{
    private Animator _anim;
    
    private void Start()
    {
        _anim = transform.GetComponent<Animator>();
    }
    
    public override void Interact()
    {
        StartCoroutine(KillDad());
    }

    private IEnumerator KillDad()
    {
        _anim.SetTrigger("fire");
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }


}
