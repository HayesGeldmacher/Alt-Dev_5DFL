using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtain : Interactable
{

    private Animator _curtain;
    private BoxCollider _collider;
    [SerializeField] private AudioSource _curtainAudio;
    [SerializeField] private bool _disappear = false;
    [SerializeField] private GameObject _disappearObject;


    // Start is called before the first frame update
    void Start()
    {
        _curtain = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        _curtain.SetTrigger("fold");
        _collider.enabled = false;
        _curtainAudio.Play();

        if(_disappear && _disappearObject != false)
        {
            _disappearObject.SetActive(false);
        }
    }

}
