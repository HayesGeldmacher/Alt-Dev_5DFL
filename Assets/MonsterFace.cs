using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterFace : Interactable
{
    private Animator _anim;
    private bool _canContinue = false;
    [SerializeField] private AudioSource _gargle;
    [SerializeField] private AudioSource _gargle2;
    [SerializeField] private AudioSource _coughSound;
    private bool _playFirstGargle = true;
    [SerializeField] private int _lines;
    [SerializeField] private ScreenshotHandler _handler;
    [SerializeField] private Animator _cursorAnim;
    [SerializeField] private nightManager _nightManage;
    private bool _dead = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_canContinue)
                {
                   if(_lines <= 7)
                    {
                    _lines++;
                     StartCoroutine(AnimateFace());
                    PlayGargle();
                    base.Interact();
                
                    if(_lines <= 1)
                        {
                            _cursorAnim.SetTrigger("clicked");
                           StartCoroutine(SetCursorVisibility());
                       
                        }

                    }
                    else
                    {
                        EndTalk();
                    }

                }
            }

        }
    }

    private IEnumerator AnimateFace()
    {
        _canContinue = false;
        _anim.SetBool("Talking", true);
        yield return new WaitForSeconds(2f);
        _anim.SetBool("Talking", false);
        _canContinue = true;
        
    }

    public void CallStartTalking()
    {
        StartCoroutine(StartTalking());
    }

    private IEnumerator StartTalking()
    {
        yield return new WaitForSeconds(0.5f);
        _anim.SetTrigger("Start");
        _coughSound.Play();
        yield return new WaitForSeconds(2f);
        _canContinue = true;
        _cursorAnim.SetBool("isCasting", true);
       
    }

        private void PlayGargle()
        {
            if (_playFirstGargle)
            {
            _playFirstGargle = false; 
                _gargle.pitch = Random.Range(0.8f, 1.2f);
                _gargle.Play();
            }
            else
            {
            _playFirstGargle = true;
                _gargle2.pitch = Random.Range(0.8f, 1.2f);
                _gargle2.Play();
            }

        }
        
    private void EndTalk()
    {
        _dead = true;
        _anim.SetTrigger("End");
        _coughSound.Play();
        _handler.ResetPicture();
        _nightManage.FreePlayer();
        base.EndDialogue();
        StartCoroutine(StartDeath());
    }

    private IEnumerator StartDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private IEnumerator SetCursorVisibility()
    {
        yield return new WaitForSeconds(0.2f);
        _cursorAnim.SetBool("isCasting", false);
    }

 }
