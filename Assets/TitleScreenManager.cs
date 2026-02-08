using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

    private bool _startedTransition = false;
    [SerializeField] private Animator _blackOutAnim;
    [SerializeField] private AudioSource _interactAudio;
    [SerializeField] private Transform _cursorSprite;
    [SerializeField] TitleScreenSpriteFollowMouse _spriteFollow;

    [SerializeField] private Datamosh _data;


    [Header("Audio Fields")]
    public bool fadeAudio = false;
    [SerializeField] private AudioSource audioToFade;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeSpeedDown;
    [SerializeField] private float minFadeUp = 0.4f;
    private bool isFading = false;
    private bool isFadingUp = false;
   

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _spriteFollow.EnableCursor(true);

        if (fadeAudio)
        {
            isFadingUp = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            
            float oldVolume = audioToFade.volume;

            if (oldVolume <= 0)
            {
                isFading = false;
                return;
            }

            float newVolume = oldVolume - (fadeSpeedDown * Time.deltaTime);
            audioToFade.volume = newVolume;

        }

        if (isFadingUp)
        {
            float oldVolume = audioToFade.volume;

            if (oldVolume >= minFadeUp)
            {
                isFadingUp = false;
                return;
            }

            float newVolume = oldVolume + (fadeSpeed * Time.deltaTime);
            audioToFade.volume = newVolume;
        }
    }


    public void CallCredits()
    {
        if (!_startedTransition)
        {
            if (fadeAudio)
            {
                isFading = true;
                isFadingUp = false;
            }

            _startedTransition = true;
            _interactAudio.Play();
            StartCoroutine(Credits());
        }
    }

    private IEnumerator Credits()
    {
        CallDataGlitch();
        _blackOutAnim.SetTrigger("fade");
        Cursor.visible = false;
        _cursorSprite.SetParent(null);
        _cursorSprite.GetComponent<Animator>().SetTrigger("fade");
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Credits");
    }

    public void CallReturnMainMenu()
    {
        if (!_startedTransition)
        {
            if (fadeAudio)
            {
                isFading = true;
                isFadingUp = false;
            }
            
            _startedTransition = true;
            _interactAudio.Play();
            StartCoroutine(ReturnMenu());
        }
    }

    private IEnumerator ReturnMenu()
    {
        CallDataGlitch();
        _blackOutAnim.SetTrigger("fade");
        Cursor.visible = false;
        _cursorSprite.SetParent(null);
        _cursorSprite.GetComponent<Animator>().SetTrigger("fade");
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("TitleScreen");
    }

    public void CallStartGame()
    {


        if (!_startedTransition)
        {
            if (fadeAudio)
            {
                isFading = true;
                isFadingUp = false;
            }

            _startedTransition = true;
            _interactAudio.Play();
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        CallDataGlitch();
        _blackOutAnim.SetTrigger("fade");
        _cursorSprite.SetParent(null);
        _cursorSprite.GetComponent<Animator>().SetTrigger("fade");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Prologue");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CallExitGame()
    {


        if (!_startedTransition)
        {
            if (fadeAudio)
            {
                isFading = true;
                isFadingUp = false;
            }
            _startedTransition = true;
            _interactAudio.Play();
            StartCoroutine(ExitGame());
        }
    }

    private IEnumerator ExitGame()
    {
        CallDataGlitch();
        _blackOutAnim.SetTrigger("fade");
        Cursor.visible = false;
        _cursorSprite.SetParent(null);
        _cursorSprite.GetComponent<Animator>().SetTrigger("fade");
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    private void CallDataGlitch()
    {
        _data.Glitch();
        _data.Glitch();
    }


    public void CallLoadDay(string day)
    {


        if (!_startedTransition)
        {
            if (fadeAudio)
            {
                isFading = true;
                isFadingUp = false;
            }
            _startedTransition = true;
            _interactAudio.Play();
            StartCoroutine(LoadDay(day));
        }
    }

    private IEnumerator LoadDay(string day)
    {
        CallDataGlitch();
        _blackOutAnim.SetTrigger("fade");
        Cursor.visible = false;
        _cursorSprite.SetParent(null);
        _cursorSprite.GetComponent<Animator>().SetTrigger("fade");
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(day);
    }
}
