using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    private DialogueManager _bedManager;
    private Dialogue _bedDialogue1;
    public Dialogue _bedDialogue2;
    [SerializeField] private EvidenceManager _evidence;
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private Transform _lookPoint;
    [SerializeField] private Transform _camHolder;
    [SerializeField] private Animator _camAnim;
    [SerializeField] private CameraController _camController;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private GameObject _monster;
    [SerializeField] private bool _spawnMonster;
    [SerializeField] private GameObject _hud;
    [SerializeField] private GameObject _cursor;
    private bool _isBedTime = false;
   
    
   

    private void Start()
    {
        _bedManager = GameManager.instance.GetComponent<DialogueManager>();
        _player = PlayerController.instance.transform;
        _bedDialogue1 = base._dialogue;
    }

    private void Update()
    {
        if (base._canWalkAway && base._startedTalking && _player)
        {
            float _distance = Vector3.Distance(_player.position, transform.position);
            if (_distance > base._dialogueDistance)
            {
                EndDialogue();
            }

        }

        if (_isBedTime)
        {
            _camHolder.position = Vector3.Lerp(_camHolder.position, _anchorPoint.position, 1 * Time.deltaTime);
            _camHolder.rotation = Quaternion.Lerp(_camHolder.rotation, _lookPoint.rotation, 1 * Time.deltaTime);
            
        }
    }

    //writing "virtual" in front of a function means that children scripts can add to/edit the function
    public override void Interact()
    {

        if (_evidence._hasEvidence)
        {
            if (_evidence._hasFoundPhone)
            {
                TriggerDialogue(_bedDialogue2);

            }
            else
            {
                TriggerDialogue(_bedDialogue1);
            }
        }
        else
        {
            TriggerDialogue(_bedDialogue1);
        }

    }

    private void TriggerDialogue(Dialogue _dialogue)
    {

        if (!base._startedTalking)
        {
            Interactable _interactable = transform.GetComponent<Interactable>();
            _bedManager.StartDialogue(_dialogue, _interactable);
            base._startedTalking = true;
        }
        else
        {
            _bedManager.DisplayNextSentence();
        }

        if(_dialogue == _bedDialogue2)
        {
            StartCoroutine(CompleteLevel());
        }
    }

    public override void EndDialogue()
    {
        _bedManager.EndDialogue();
    }

    private IEnumerator CompleteLevel()
    {
        _camHolder.parent = null;
        _camController.enabled = false;
        PlayerController.instance.enabled = false;
       Destroy( PlayerController.instance.transform.gameObject);
        _isBedTime = true;
        yield return new WaitForSeconds(2);
        _blackAnim.SetTrigger("black");
        EndDialogue();
        yield return new WaitForSeconds(4);
        if (_spawnMonster)
        {
            _hud.SetActive(false);
            _cursor.SetActive(false);
            _monster.SetActive(true);
            _blackAnim.SetTrigger("blinking");
            yield return new WaitForSeconds(4);
            GameManager.instance.LoadNextLevel();

        }
        else
        {
        GameManager.instance.LoadNextLevel();

        }

    }

}
