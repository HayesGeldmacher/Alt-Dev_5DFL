using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class IntroManager : Interactable
{

    public bool _canInteract = false;
    [SerializeField] private float _interactWait;
    private float _currentInteractWait;
    [SerializeField] private Animator _textAnim;
    [SerializeField] private AudioSource _interactSound;
    public int _dialogueLength;
    public int _audioTriggerChange1;
    [SerializeField] private Animator _blackOutAnim;

    [SerializeField] private AudioSource _ambientAudioLight;
    [SerializeField] private AudioSource _ambientAudioDark;

    [SerializeField] private Animator _houseAnim;
    [SerializeField] private Animator _audioAnim;



    // Start is called before the first frame update
    void Start()
    {
        _currentInteractWait = 0;
        _textAnim.SetBool("active", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canInteract)
        {
            _currentInteractWait -= Time.deltaTime;
            if(_currentInteractWait <= 0)
            {
                _canInteract = true;
            }
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (_canInteract)
            {
                Interact();
            }
        }
    }

    private void Interact()
    {
        _dialogueLength -= 1;
        if(_dialogueLength == _audioTriggerChange1)
        {
           AudioTriggerChange();
        }
        if(_dialogueLength <= 0)
        {
            StartCoroutine(EndScene());
        }
        else
        {
      
        base.Interact();
        _interactSound.Play();
        _currentInteractWait = _interactWait;
        }

    }

    private void AudioTriggerChange()
    {
        _ambientAudioLight.Stop();
        _ambientAudioDark.Play();
        _houseAnim.SetTrigger("dark");
    }

    private IEnumerator EndScene()
    {
        _blackOutAnim.SetTrigger("blackIntro");
        _audioAnim.SetTrigger("fade");
        _textAnim.SetBool("active", false);
        yield return new WaitForSeconds(3);
        //play a sound here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
