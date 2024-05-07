using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWall : Interactable
{
     private MeshRenderer _render;
     private BoxCollider _bc;
    [SerializeField] private Animator _glimmer;
    [SerializeField] private AudioSource _crumble;
    [SerializeField] private Animator _crumbleAnim;

    private void Start()
    {
        base.Start();
        _render = transform.GetComponent<MeshRenderer>();
        _bc = transform.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
       base.Interact();
       StartCoroutine(BreakWall());
    }

    private IEnumerator BreakWall()
    {
        yield return new WaitForSeconds(1.5f);
        _crumbleAnim.SetTrigger("crumble");
        _render.enabled = false;
        _bc.enabled = false;
        _glimmer.SetTrigger("fade");
        _crumble.Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
