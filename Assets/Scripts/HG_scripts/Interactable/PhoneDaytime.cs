using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneDaytime : Interactable
{

    public float speed;
    [SerializeField]public bool _isDarkening = false;
    [SerializeField]public Color colorStart = Color.blue;
    [SerializeField]public Color colorEnd = Color.green;
    private DialogueManager _dialogueManager;
    private Dialogue _defaultDialogue;
    public Dialogue _phoneDialogue;
    [SerializeField] private EvidenceManager _evidence;
    [SerializeField] private AudioSource _garble1;
    [SerializeField] private AudioSource _garble2;
    [SerializeField] private AudioSource _garble3;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _lightShaft1;
    [SerializeField] private GameObject _lightShaft2;
    [SerializeField] private Material _skyBoxMat;
    [SerializeField] private GameObject _pointWindowLight;
    private float t;
    float duration = 4;
    private bool _hasSpawned = false;
    private int diaNum = 0;
    [SerializeField] private bool _canAudio = true;
    private bool _hasPlayed = false;

    private void Start()
    {
        //base.Start();
        base.Start();
        _dialogueManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _defaultDialogue = base._dialogue;
        //colorStart = _skyBoxMat.TintColor;
        _isDarkening = false;
        RenderSettings.skybox.SetColor("_Tint", colorStart);
        t = 0;
    }

    private void Update()
    {

        base.Update();
        if (base._startedTalking && base._isTimed)
        {

        }

        if (base._canWalkAway && base._startedTalking && _player)
        {
            float _distance = Vector3.Distance(_player.position, transform.position);
            if (_distance > base._dialogueDistance)
            {
                EndDialogue();
            }

        }

        if (_isDarkening)
        {
           Color lerpedColor = Color.Lerp(colorStart, colorEnd, t);
            // renderer.material.color = lerpedColor
            RenderSettings.skybox.SetColor("_Tint", lerpedColor);
            t += Time.deltaTime / duration;

           // RenderSettings.skybox.SetColor("_Tint", lerpedColor);
            //RenderSettings.skybox.SetColor("_Tint", Color.black);
        }
    }

    //writing "virtual" in front of a function means that children scripts can add to/edit the function
    public override void Interact()
    {
        base.currentDialogueTime = base._dialogueTimer;

        if (_evidence._hasEvidence)
        {
            TriggerDialogue(_phoneDialogue);
            diaNum += 1;

            if(diaNum == 1)
            {
                if (_garble1)
                {
                    _garble1.Play();
                    _garble2.Stop();
                    _garble3.Stop();
                }
            }
            else if(diaNum == 2)
            {
                if (_garble2)
                {
                    _garble1.Stop();
                    _garble3.Stop();
                    _garble2.Play();
                }
            }
            else if(diaNum == 3)
            {
                if (_garble3)
                {
                    _garble1.Stop();
                    _garble2.Stop();
                    _garble3.Play();
                }
            }
        }
        else
        {

            TriggerDialogue(_defaultDialogue);
        }
    }

    private void TriggerDialogue(Dialogue _dialogue)
    {
        if (_dialogue == _phoneDialogue)
        {
            _evidence._hasFoundPhone = true;

        }

        if (!base._startedTalking)
        {
            _hasPlayed = false;
            if (_dialogue == _phoneDialogue)
            {
                if (_garble1)
                {
               // _garble1.Play();
                }
            }

            Interactable _interactable = transform.GetComponent<Interactable>();
            _dialogueManager.StartDialogue(_dialogue, _interactable);
            base._startedTalking = true;
        }
        else
        {
            _dialogueManager.DisplayNextSentence();

            if (_dialogue == _phoneDialogue && !_hasPlayed)
            {

                _anim.SetTrigger("fade");
                StartCoroutine(Darkness());
                if (_garble2)
                {
                _garble2.Play();
                }
                _hasPlayed = true;
                
            }

        }

    }

    private IEnumerator Darkness()
    {
       
        yield return new WaitForSeconds(3);
        _isDarkening = true;
        Destroy(_pointWindowLight);
        yield return new WaitForSeconds(1);
        Destroy(_lightShaft1);
        Destroy(_lightShaft2);
    }


    public override void EndDialogue()
    {
        _dialogueManager.EndDialogue();

    }

    private void SunDown()
    {

    }

}
