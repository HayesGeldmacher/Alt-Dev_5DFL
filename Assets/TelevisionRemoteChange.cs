using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelevisionRemoteChange : MonoBehaviour
{
    public float _clickCoolDown;
    private bool _canClick;
    private float _currentCoolDown;
    
    public int _currentChannel;
    public int _maxChannels;

    public Animator _televisionAnim;

    public Channel[] _channels;

    public GameObject _currentScreen;

    public int _requiredClicks;
    public int _currentClicks;
    private bool _ended = false;

    public Animator _camAnim;
    public Animator _fadeAnim;

    private AudioSource _clickSound;

    public AudioSource _blipSound;
    public AudioClip[] _blipClips;

    // Start is called before the first frame update
    void Start()
    {
        _clickSound = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (_canClick)
            {
                _canClick = false;
                _clickSound.pitch = Random.Range(0.85f, 1.15f);
                _clickSound.Play();
                _currentCoolDown = _clickCoolDown;
                ChannelChange();
                PlayBlip();
                Debug.Log("CHangled the channel!");
            }
        }

        if (!_canClick)
        {
            _currentCoolDown -= Time.deltaTime;
            if(_currentCoolDown <= 0)
            {
                _canClick = true;
            }
        }
    }

    private void PlayBlip()
    {
        _blipSound.clip = _blipClips[Random.Range(0, _blipClips.Length)];
        _blipSound.pitch = Random.Range(0.85f, 1.15f);
        _blipSound.Play();
    }

    private void ChannelChange()
    {
        _currentClicks += 1;
        if(_currentClicks >= _requiredClicks)
        {
            if (!_ended)
            {
                StartCoroutine(BeginEnding());
            }
        }
        
        _currentChannel += 1;
        if (_currentChannel > _maxChannels)
        {
            _currentChannel = 0;
        }

        _currentScreen.SetActive(false);
        _currentScreen = _channels[_currentChannel].screen;
        _currentScreen.SetActive(true);

    }


    private IEnumerator BeginEnding()
    {
        //2 anims - one for eyes closing, one for camera kinda swaying back and forth!
        //FIND ANIMATIONS FOR BLACKOUT AND CAMERA FROM OTHER SCENE!
        //Animate eyes closing here, do the same swaying animation as before - 

        //Cam - swivel, HG_ANimator_Camera
        //black fade in
        _fadeAnim.SetTrigger("long");
        yield return new WaitForSeconds(2.5f);
        _camAnim.enabled = true;
        _camAnim.SetTrigger("sleep");
        yield return new WaitForSeconds(1);
        _fadeAnim.SetTrigger("blinking");
        yield return new WaitForSeconds(5f);
        EndScene();

    }

    public void EndScene()
    {
        //transition to next scene - 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
