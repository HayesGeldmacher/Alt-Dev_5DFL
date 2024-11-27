using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadPrologueInteraction : Interactable
{

    [SerializeField] private List<AudioClip> _dialogueClips = new List<AudioClip>();
    [SerializeField] private AudioSource _audio;
    [SerializeField] private int _currentLine;
    [SerializeField] private int _totalLines;
    [SerializeField] private Animator _dadAnim;
    private bool _startedInteraction = false;
    

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
            PlayerController.instance._frozen = true;
        }
        
            _dadAnim.SetTrigger("talk");
        
        base.Interact();
        if(_currentLine <= _dialogueClips.Count - 1)
        {
            PlaySound();
        }
        
        if(_currentLine >= _totalLines)
        {
            _currentLine = 0;
            _startedInteraction = false;
            PlayerController.instance._frozen = false;
            _dadAnim.SetTrigger("end");
        }
        else
        {
        _currentLine++;
        }
        

        
    }

    public override void EndDialogue()
    {
        base.EndDialogue();
        _currentLine = 0;
    }

    private void PlaySound()
    {
        _audio.clip = _dialogueClips[_currentLine];
        _audio.Play();
    }
}
