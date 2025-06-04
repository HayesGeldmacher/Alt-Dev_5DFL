using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monitor3rdPerson : Interactable
{
    private bool _canEnter = true;
    [SerializeField] private CutsceneManager _scene;
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
    private void Update()
    {
        base.Update();
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
            _mosh.CallGlitch();
            _mosh.CallGlitch();
            GameManager.instance.PlayInteractSound();
            _player3rd._frozen = true;
            _started = true;
            StartCoroutine(StartDream());
        }
    }

    private IEnumerator StartDream()
    {
        _blackoutAnim.SetTrigger("long");
        _mosh.CallGlitch();
        yield return new WaitForSeconds(0.2f);
            _mosh.CallGlitch();
        _dialogueBox.SetActive(false);
        yield return new WaitForSeconds(1f);
        _blackoutAnim.SetTrigger("out");
        _interstitialCanvas.SetActive(true);
        _camOverlay.SetActive(false);
            _breatheAudio.Stop();
        _staticAudio.Play();
        _UICam.SetActive(true);
        yield return new WaitForSeconds(1f);
            _mosh.CallGlitch();
        _scene.BeginDoor();
    }
}
