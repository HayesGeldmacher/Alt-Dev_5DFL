using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadFinaleInteraction : Interactable
{

    [SerializeField] private List<AudioClip> _dialogueClips = new List<AudioClip>();
    [SerializeField] private AudioSource _audio;
    [SerializeField] private int _currentLine;
    [SerializeField] private int _totalLines;
    [SerializeField] private Animator _dadAnim;
    private bool _startedInteraction = false;
    private bool _animToPlay = false;


    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _crowdSound;
    [SerializeField] private AudioSource _laughSound;
    [SerializeField] private AudioSource _breathSound;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {


        if (!_startedInteraction)
        {
            _startedInteraction = true;
            _breathSound.Stop();
        }

        if (_animToPlay)
        {
            _animToPlay = false;
            _dadAnim.SetTrigger("talk2");
        }
        else
        {
            _animToPlay = true;
            _dadAnim.SetTrigger("talk1");
        }

        base.Interact();
        if (_currentLine <= _dialogueClips.Count - 1)
        {
            PlaySound();
        }


    }

    public override void EndDialogue()
    {
        base.EndDialogue();
        _currentLine = 0;
    }

    private void PlaySound()
    {
        _audio.clip = _dialogueClips[Random.Range(0, _dialogueClips.Count)];
        _audio.Play();
    }

}
