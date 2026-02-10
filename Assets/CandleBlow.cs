using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleBlow : MonoBehaviour
{

    public float clickCoolDown;
    public float _totalCoolDown;
    public bool _canClick = false;
    public bool _tooSleepy = false;

    public Animator[] _fireAnims;

    public int _blowCount = 0;
    public int _totalBlow;

    public float[] candleBlowTimes;

    public bool _candleOut = false;

    public Animator _puppet;
    
    
    [Header("Dialogue Fields")]
    public Interactable _dialogue;
    public bool _started = false;
    public int _currentDialogue = 0;
    public int _totalDialogue = 2;
    public Animator _cursor;

    public  Dialogue _puppetStatements;
    public Dialogue _blankStatements;

    private bool _canStart = false;

    public Animator _blackOut;

    public CRT crt;

    [Header("Audio Fields")]
    public AudioSource _interactAudio;
    public AudioSource _birthday;
    public AudioSource _blowAudio;
    public AudioSource _areYouThere;
    public AudioSource _ambientAudio;
    private bool ambientFading = false;
    [SerializeField] private float fadeSpeed = 0.1f;
    
  
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        _blackOut.SetTrigger("blackInstant");
        StartCoroutine(BeginScene());
    }

    private IEnumerator BeginScene()
    {
        crt.StartBreathing();
        yield return new WaitForSeconds(1f);
        _blackOut.SetTrigger("fade");
        crt.EndBreathing();
        yield return new WaitForSeconds(3f);
        _canStart = true;
        _cursor.SetBool("appear", true);
    }
 
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance._isPaused) return;

        if (!_started)
        {
            if (Input.GetButtonDown("Interact") && _canStart)
            {
                _dialogue.Interact();
                PlayInteractSound();
                _currentDialogue++;
                _cursor.SetTrigger("click");
                _puppet.SetTrigger("trigger");

                if(_currentDialogue >= _totalDialogue)
                {
                    _started = true;
                }
            }
        }
        else
        {
            BlowUpdate();
        }


        if (ambientFading)
        {
            float currentVolume = _ambientAudio.volume;
            float newVolume = currentVolume - (fadeSpeed * Time.deltaTime);
            _ambientAudio.volume = newVolume;
            if(newVolume <= 0) { ambientFading = false; }
        }
    }

    private void BlowUpdate()
    {
            if (_canClick)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (!_tooSleepy)
                    {
                        PlayInteractSound();    
                        BlowCandle();
                    }    
                }
            }
            else
            {
                if (!_candleOut)
                {
                        clickCoolDown -= Time.deltaTime;
                        if(clickCoolDown <= 0)
                        {
                            _canClick = true;
                        }
                }    
            }

    }

    private void BlowCandle()
    {   
        _dialogue.Interact();
        clickCoolDown = _totalCoolDown;
        _canClick = false;
        _cursor.SetTrigger("click");
        _cursor.SetBool("appear", false);
        PlayBlowSound();
        
        //make the CRT breathe shit happpen!

        if (_blowCount < _totalBlow)
        {
            StartCoroutine(BringBack(candleBlowTimes[_blowCount]));
            _blowCount++;
        }
        else
        {
            if (!_tooSleepy)
            {
                StartCoroutine(Darkness());

            }
        }

    }

    private IEnumerator BringBack(float wait)
    {
        _candleOut = true;
        crt.StartBreathing();

        _dialogue._dialogue = _blankStatements;
        _dialogue._dialogue._sentences[0] = _puppetStatements._sentences[_blowCount];
        _currentDialogue++;

        yield return new WaitForSeconds(3);
        foreach (Animator flame in _fireAnims)
        {
            flame.SetTrigger("fade");
        }


        yield return new WaitForSeconds(wait);
        _puppet.SetInteger("react", _blowCount);
        _puppet.SetTrigger("trigger");

        foreach (Animator flame in _fireAnims)
        {
            flame.SetTrigger("back");
        }

        crt.EndBreathing();
        yield return new WaitForSeconds(1);
       _dialogue.Interact();
        
        _cursor.SetBool("appear", true);
        _candleOut = false;
    }
         
    private IEnumerator Darkness()
    {
        _tooSleepy = true;
        crt.StartBreathing();

        yield return new WaitForSeconds(3);
        _birthday.Stop();
        foreach (Animator flame in _fireAnims)
        {
            flame.SetTrigger("fade");
        }
        Debug.Log("StartedDarkness!");
        _dialogue._dialogue = _blankStatements;
        _dialogue._dialogue._sentences[0] = _puppetStatements._sentences[_blowCount];
        yield return new WaitForSeconds(6);
        _dialogue.Interact();
        _areYouThere.Play();
        yield return new WaitForSeconds(5);
        _dialogue.Interact();
        ambientFading = true;
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void PlayInteractSound()
    {
        _interactAudio.pitch = Random.Range(0.8f, 1.1f);
        _interactAudio.Play();
    }

    private void PlayBlowSound()
    {
        _blowAudio.pitch = Random.Range(0.8f, 1.1f);
        _blowAudio.Play();
    }
}
