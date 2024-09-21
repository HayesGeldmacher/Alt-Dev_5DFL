using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDadShoot : Interactable
{
   
    private bool _hasTalked = false;
    [SerializeField] private Animator _anim;

    [SerializeField] private int _currentDialogueNum;
    [SerializeField] private int _totalDialogueNum;

    private int garbleNum = 0;
    [SerializeField] private AudioSource _garble1;
    [SerializeField] private AudioSource _garble2;
    [SerializeField] private AudioSource _garble3;

    [SerializeField] private GameObject _dadShootTrigger;

    private bool _finishedTalking = false;
    
    // Start is called before the first frame update
    void Start()
    {
       base.Start();
        _hasTalked = false;
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (_finishedTalking) return;

        base.Interact();


        if (!_hasTalked)
        {
            _hasTalked = true;
            _anim.SetTrigger("peek");
        }


        

        if (garbleNum == 0)
        {
            garbleNum++;
            PlayGarbleSound(_garble1);
        }
        else if(garbleNum == 1)
        {
            garbleNum++;
            PlayGarbleSound(_garble2);
        }
        else
        {
            garbleNum = 0;
            PlayGarbleSound(_garble3);
        }

        _currentDialogueNum++;

        if(_currentDialogueNum > (_totalDialogueNum))
        {
            
            LineUp();
        }


    }


    private void LineUp()
    {
        _finishedTalking = true;
        _anim.SetTrigger("line");
        _player = PlayerController.instance.transform;
        _player.GetComponent<PlayerController>()._frozen = false;
    }

    private void PlayGarbleSound(AudioSource _audio)
    {
        _audio.pitch = Random.Range(0.8f, 1.2f);
        _audio.Play();
    }

    public void SpawnShootTrigger()
    {

        _dadShootTrigger.SetActive(true);
        gameObject.SetActive(false);
    }
}
