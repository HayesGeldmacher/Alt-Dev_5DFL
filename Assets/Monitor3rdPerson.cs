using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monitor3rdPerson : Interactable
{
    private bool _canEnter = true;
    [SerializeField] private CutsceneManager _scene;
    [SerializeField] private StartDataMosh _startMosh;
    private bool _started = false;
    [SerializeField] private PlayerController _player3rd;
    [SerializeField] private StartDataMosh _mosh;
    [SerializeField] private AudioSource _breatheAudio;
    [SerializeField] private GameObject _UICam;
    [SerializeField] private GameObject _camOverlay;
    [SerializeField] private GameObject _interstitialCanvas;
    [SerializeField] private Animator _blackoutAnim;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private AudioSource _staticAudio;

    public bool _lerpVolume = false;
    public bool _lerpVolumeStatic = false;
    public float _lerpSpeed = 0.1f;

    private void Update()
    {
        base.Update();
        if (_lerpVolume)
        {
            _breatheAudio.volume -= _lerpSpeed * Time.deltaTime;
            if(_breatheAudio.volume <= 0)
            {
                _lerpVolume = false;
            }
        }

        if (_lerpVolumeStatic)
        {
            _staticAudio.volume -= _lerpSpeed * Time.deltaTime;
            if (_staticAudio.volume <= 0)
            {
                _lerpVolumeStatic = false;
            }
        }

    }


    public void OnTriggerEnter(Collider other)
    {
        if (_canEnter)
        {
            if (other.tag == "Player")
            {
                Interact();
            }
        }

    }

    public override void Interact()
    {

        if (!_started)
        {
            GameManager.instance.PlayInteractSound();
            _player3rd._frozen = true;
            _started = true;
            StartCoroutine(StartDream());
        }
    }

    private IEnumerator StartDream()
    {
        _blackoutAnim.SetTrigger("long");
        yield return new WaitForSeconds(0.2f);
        _dialogueBox.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        _blackoutAnim.SetTrigger("out");
        _interstitialCanvas.SetActive(true);
        _camOverlay.SetActive(false);
        _lerpVolume = true;
        _staticAudio.Play();
        _UICam.SetActive(true);
        yield return new WaitForSeconds(1f);
            _mosh.CallGlitch();
        _scene.BeginDoor();

    }

    
}
