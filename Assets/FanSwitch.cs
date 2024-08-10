using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FanSwitch : Interactable
{

    [SerializeField] private Animator _switchAnim;
    [SerializeField] private Animator _fanAnim;
    private bool _on = false;
    private AudioSource _click;
    
    // Start is called before the first frame update
    void Start()
    {
        _on = false;
        _click = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        _click.Play();

        if (_on)
        {
            _on = false;
            _switchAnim.SetBool("on", false);
            _fanAnim.SetBool("on", false);
        }
        else
        {
            _on = true;
            _switchAnim.SetBool("on", true);
            _fanAnim.SetBool("on", true);
            Debug.Log("MEEYA");

        }
    }
}
