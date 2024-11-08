using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtain : Interactable
{

    private Animator _curtain;
    private BoxCollider _collider;
    [SerializeField] private GameObject _mannequin;
    [SerializeField] private AudioSource _curtainAudio;


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
        _mannequin.SetActive(false);
        _curtain.SetTrigger("fold");
        _collider.enabled = false;
        _curtainAudio.Play();
    }

}
