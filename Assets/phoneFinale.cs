using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class phoneFinale : Interactable
{
    private bool _finishedTalking = false;


    [SerializeField] private AudioSource _ringingSource;
    [SerializeField] private int _dialogueNum;
    [SerializeField] private AudioSource _phonePickupSound;
    [SerializeField] private AudioSource _phonePutDownSound;

    [SerializeField] private bool _pickedUp = false;
    [SerializeField] private bool _finished = false;

    [SerializeField] private GameObject _wallDisappear;
    [SerializeField] private Dialogue _dialogueEnd;

    [SerializeField] private CameraController _camController;
    public Animator phoneAnim;
    // Start is called before the first frame update
    void Start()
    {
        phoneAnim.SetTrigger("on");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {


        if (_finished)
        {
            base._dialogue = _dialogueEnd;
            base.Interact();
        }
        else
        {
            base.Interact();

            if (!_pickedUp)
            {
                _phonePickupSound.Play();
                phoneAnim.SetTrigger("talking");
                _pickedUp = true;
                _ringingSource.Stop();
                PlayerController.instance._frozen = true;
               // _camController._frozen = true;
                
            }

            _dialogueNum -= 1;

            if(_dialogueNum <= 0)
            {
                EndPhoneCall();
            }
        }
    }

    private void EndPhoneCall()
    {
        _finished = true;
        phoneAnim.SetTrigger("off");
        _phonePutDownSound.Play();
        _wallDisappear.SetActive(false);
        PlayerController.instance._frozen = false;
        _camController._frozen = false;
    }
}
