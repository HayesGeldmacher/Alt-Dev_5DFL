using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVNewsReport : Interactable
{

    [SerializeField] private Animator _tvAnimation;
    [SerializeField] private CameraController _camController;
    private bool _canInteract = true;
    private bool _startedInteraction = false;


    [SerializeField] private List<AudioClip> _soundClips = new List<AudioClip>();
    [SerializeField] private AudioSource _backgroundAudio;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioSource _turnOffTV;
    [SerializeField] private AudioSource _interactSound;
    public int _dialogueLines = 0;


    [SerializeField] private GameObject _currentTelevision;
    [SerializeField] private GameObject _frozenTelevision;

    [SerializeField] private GameObject _monitorNormal;
    [SerializeField] private GameObject _monitorEyes;

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
                _interactSound.Play();

                if (_dialogueLines <= 3)
                {
                    base.Interact();
                    PlaySound();
                }

                if(_dialogueLines == 3)
                {
                    _tvAnimation.SetTrigger("stare");
                    _backgroundAudio.loop = false;
                    _backgroundAudio.Stop();
                }
                else if(_dialogueLines > 3)
                {
                    _tvAnimation.SetTrigger("exit");
                }


                if(_dialogueLines > 3)
                {
                    _dialogueLines = 0;

                    StartCoroutine(InteractDelay());
                    _camController._frozen = false;
                    PlayerController.instance._frozen = false;
                    _turnOffTV.Play();
                    _currentTelevision.SetActive(false);
                    _frozenTelevision.SetActive(true);

                    _monitorNormal.SetActive(false);
                    _monitorEyes.SetActive(true);
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
        if (!_startedInteraction)
        {
        
        PlaySound();
        _backgroundAudio.Play();
        _backgroundAudio.loop = true;
             base.Interact();
             _startedInteraction = true;
        _tvAnimation.SetTrigger("play");
        _camController._frozen = true;
        PlayerController.instance._frozen = true;
                _dialogueLines++;

        }


    }

    public override void EndDialogue()
    {

    }

    private void PlaySound()
    {
        if (_dialogueLines < _soundClips.Count)
        {
        AudioClip _chosenClip = _soundClips[_dialogueLines];
        _audio.clip = _chosenClip;
        _audio.Play();

        }

    }

    private IEnumerator InteractDelay()
    {
        yield return new WaitForSeconds(1);
        _startedInteraction = false;
        base.EndDialogue();
    }


}
