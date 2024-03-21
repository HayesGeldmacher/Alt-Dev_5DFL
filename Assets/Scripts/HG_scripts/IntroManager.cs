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
    public float _dialogueLength;
    [SerializeField] private Animator _blackOutAnim;

    // Start is called before the first frame update
    void Start()
    {
        _currentInteractWait = 0
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
                StartCoroutine(Interact());
            }
        }
    }

    private IEnumerator Interact()
    {
        _dialogueLength -= 1;
        if(_dialogueLength <= 0)
        {
            StartCoroutine(EndScene());
        }
        else
        {
      
        base.Interact();
        _interactSound.Play();
        _currentInteractWait = _interactWait;
        yield return new WaitForSeconds(1);
        }

    }

    private IEnumerator EndScene()
    {
        _blackOutAnim.SetTrigger("blackIntro");
        yield return new WaitForSeconds(3);
        //play a sound here
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
