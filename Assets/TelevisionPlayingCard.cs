using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelevisionPlayingCard : Interactable
{

    [Header("Card Fields")]
    public Animator _backGroundAnim;
    public Animator _foreGroundAnim;
    public int _key;
    public Animator _cursorAnim;
    private bool _started = false;
    private bool _finished = false;
    private bool _waited = false;

    [SerializeField] private int _dialogueNum;
    public  int _totalDialogueNum;
    [SerializeField] private CameraController _camController;
    
    [Header("Teleport Fields")]
    [SerializeField] private Transform _teleportPosition;
    [SerializeField] private Transform _teleportRotation;
    [SerializeField] private Transform _playerTransform;

    public TelevisionCardRoom _cardRoom;
    public AudioSource _audio;
    public AudioSource _talkAudio;
    public AudioClip[] _audioClips;


    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if(_waited && _started)
        {
            if (Input.GetButtonDown("Interact"))
            {
                TriggerDialogue();
            }
        }
        
    }

    private void TriggerDialogue()
    {
        _dialogueNum++;
        _cursorAnim.SetTrigger("click");
        GameManager.instance.PlayInteractSound();
        if(_dialogueNum <= _totalDialogueNum)
        {
            PlayTalkAudio();
            base.Interact();
        }
        else
        {
            base.EndDialogue();

            StartCoroutine(EndScreen());
        }
    }

    public override void Interact()
    {
        if (!_started)
        {
            _started = true;
            StartCoroutine(StartScreen());
        }
        
       

    }

    private IEnumerator EndScreen()
    {
        _cursorAnim.SetBool("appear", false);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(1.0f);
        PlayerController.instance._frozen = false;
        _camController._frozen = false;
        TeleportPlayer();
        _foreGroundAnim.SetTrigger("off");
        _backGroundAnim.SetTrigger("off");
        _waited = false;
        _started = false;
        _dialogueNum = 0;
        _cardRoom.IterateCard();
        yield return new WaitForSeconds(2f);
        _audio.Stop();

    }

    public IEnumerator StartScreen()
    {
        PlayerController.instance._frozen = true;
        _camController._frozen = true;
        _backGroundAnim.SetTrigger("on");
        _foreGroundAnim.SetInteger("key", _key);
        _foreGroundAnim.SetTrigger("on");
        _audio.Play();
        yield return new WaitForSeconds(1.5f);
        _waited = true;
        _cursorAnim.SetBool("appear", true);

    }

    private void TeleportPlayer()
    {

        Vector3 relativePos = _teleportRotation.position - _playerTransform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        _playerTransform.localRotation = Quaternion.Euler(_playerTransform.localRotation.x, relativePos.y, _playerTransform.localRotation.z);

        _playerTransform.gameObject.SetActive(false);
        _playerTransform.position = _teleportPosition.position;
        _playerTransform.gameObject.SetActive(true);
    }

    private void PlayTalkAudio()
    {
        int randomChoice = Random.Range(0, _audioClips.Length);
        AudioClip clip = _audioClips[randomChoice];
        _talkAudio.clip = clip;
        _talkAudio.pitch = Random.Range(0.8f, 1.2f);
        _talkAudio.Play();
    }
}
