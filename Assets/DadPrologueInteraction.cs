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
    private bool _animToPlay = false;


    [SerializeField] private GameObject _fakeBed;
    [SerializeField] private GameObject _realBed;

    [SerializeField] private StartDataMosh _mosh;
    private bool _startedKillDad = false;

    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _crowdSound;
    [SerializeField] private AudioSource _laughSound;

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
        if(_currentLine <= _dialogueClips.Count - 1)
        {
            PlaySound();
        }
        
        if(_currentLine >= _totalLines)
        {
            KillDad();

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

    private void KillDad()
    {
        _currentLine = 0;
        _startedInteraction = false;
        PlayerController.instance._frozen = false;
        _realBed.SetActive(true);
        _fakeBed.SetActive(false);
        _dadAnim.SetTrigger("shoot");
        transform.GetComponent<BoxCollider>().enabled = false;
        _mosh.CallGlitch();
       
    }

    public void CallDeath()
    {
        if (!_startedKillDad)
        {
            _startedKillDad = true;
            StartCoroutine(Death());
        }

    }

    private IEnumerator Death()
    {
        _mosh.CallGlitch();
        _mosh.CallGlitch();
        _shootSound.Play();
        _crowdSound.Play();
        _laughSound.Play();
        yield return new WaitForSeconds(0.8f);
        _mosh.CallGlitch();
        Destroy(gameObject);

    }


}
