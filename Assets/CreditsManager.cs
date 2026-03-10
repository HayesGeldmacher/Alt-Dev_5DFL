using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Animator _camAnim;


    [Header("Credits Fields")]
    [SerializeField] private float _creditCountDown;
    [SerializeField] private float _currentCreditCountDown;
    private float _currentScrollSpeed;
    [SerializeField] private float _creditScrollSpeed;
    [SerializeField] private float _creditScrollSpeedFast;
    [SerializeField] private bool _countingDown;

    [SerializeField] private float _creditStartPosition;
    [SerializeField] private float _creditEndPosition;
    [SerializeField] private float _currentCreditsPosition;

    [SerializeField] private bool _creditsScrolling;

    [SerializeField] private Transform _credits;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private Animator _textAnim;


    [SerializeField] private AudioSource gameMusic;

    // Start is called before the first frame update
    void Start()
    {
        _blackAnim.SetTrigger("blackInstant");
        StartCoroutine(StartScene());
    }

    private void Update()
    {
        if (_countingDown)
        {
            _currentCreditCountDown -= Time.deltaTime;
            if(_currentCreditCountDown <= 0)
            {
                _countingDown = false;
                StartCoroutine(BeginCreditsScrolling());
            }
        }

        if (_creditsScrolling)
        {
            float creditPos = _credits.localPosition.y;

            if (Input.GetButton("Interact"))
            {
                _currentScrollSpeed = _creditScrollSpeedFast;
            }
            else
            {
                _currentScrollSpeed = _creditScrollSpeed;
            }
                creditPos += (Time.deltaTime * _currentScrollSpeed);
            


            Vector3 newPos = new Vector3(_credits.localPosition.x, creditPos, _credits.localPosition.z);
            _credits.localPosition = newPos;
            _currentCreditsPosition = _credits.localPosition.y; 

            if(_currentCreditsPosition >= _creditEndPosition)
            {
                _creditsScrolling = false;
                StartCoroutine(EndScene());
            }

        }
    }

    private IEnumerator StartScene()
    {
        //tv turns on
        //then camera slowly sways away
        //then the "ritual static" logo comes up
        //then credits play
        yield return new WaitForSeconds(5f);
        _blackAnim.SetTrigger("fade");
        yield return new WaitForSeconds(5f);
        _camAnim.SetTrigger("sway");
        
    }

    public void BeginCreditsCountdown()
    {
        _countingDown = true;
    }

    public IEnumerator BeginCreditsScrolling()
    {
        gameMusic.Play();
        _textAnim.SetTrigger("fade");
        yield return new WaitForSeconds(5.5f);
        _creditsScrolling = true;
    }

    private IEnumerator EndScene()
    {
        _blackAnim.SetTrigger("veryLong");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("TitleScreen");
    }
}
