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

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_canContinue)
            {
               if(_lines <= 8)
                {
                _lines++;
                 StartCoroutine(AnimateFace());
                PlayGargle();
                base.Interact();

                }
                else
                {
                    EndTalk();
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
        yield return new WaitForSeconds(1f);
        _canContinue = true;
    }

        private void PlayGargle()
        {
            if (_playFirstGargle)
            {
                _gargle.pitch = Random.Range(0.8f, 1.2f);
                _gargle.Play();
            }
            else
            {
                _gargle2.pitch = Random.Range(0.8f, 1.2f);
                _gargle2.Play();
            }

        }
        
    private void EndTalk()
    {
        _anim.SetTrigger("End");
        _coughSound.Play();
    }

    }
