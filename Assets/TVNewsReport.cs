using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVNewsReport : Interactable
{

    [SerializeField] private Animator _tvAnimation;
    [SerializeField] private int _dialogueLines = 0;
    [SerializeField] private CameraController _camController;
    private bool _canInteract = true;
    private bool _startedInteraction = false;


    [SerializeField] private List<AudioClip> _soundClips = new List<AudioClip>();
    [SerializeField] private AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) && Time.timeScale > 0)
        {
            if (_startedInteraction)
            {
                base.Interact();
                PlaySound();

                if(_dialogueLines == 3)
                {
                    _tvAnimation.SetTrigger("stare");
                }
                else if(_dialogueLines > 3)
                {
                    _tvAnimation.SetTrigger("exit");
                }


                if(_dialogueLines > 3)
                {
                    _dialogueLines = 0;

                    _startedInteraction = false;
                    _camController._frozen = false;
                    PlayerController.instance._frozen = false;
                }
                else
                {
                _dialogueLines++;
                }

            }
          

        }
    }

    private void ContinueInteract()
    {

    }

    public override void Interact()
    {
             base.Interact();
             _startedInteraction = true;
        _tvAnimation.SetTrigger("play");
        _camController._frozen = true;
        PlayerController.instance._frozen = true;
                _dialogueLines++;
        PlaySound();


    }

    public override void EndDialogue()
    {

    }

    private void PlaySound()
    {
        
        AudioClip _chosenClip = _soundClips[_dialogueLines];
        _audio.Play();
    }


}
