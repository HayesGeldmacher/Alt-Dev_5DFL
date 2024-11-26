using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedNight : Interactable
{

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
    [SerializeField] private GameObject _television;
    private bool _startedEnd = false;
    private bool _isBedTime = false;
    [SerializeField] private CameraZoom _camZoom;
    [SerializeField] private Door _door;
    [SerializeField] private Animator _swivel;
    



    private void Start()
    {
        _player = PlayerController.instance.transform;
        base.Start();
    }

    private void Update()
    {
       
        base.Update();
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

  
    public override void Interact()
    {

        if (!_startedEnd)
        {
            if (!_door._isOpen)
            {
            _startedEnd = true;
            StartCoroutine(CompleteLevel());
            }
            else
            {
                base.Interact();
            }
        }


    }

    private IEnumerator CompleteLevel()
    {
        _camHolder.parent = null;
        _camController.enabled = false;
        PlayerController.instance.enabled = false;
        Destroy(PlayerController.instance.transform.gameObject);
        _isBedTime = true;
        GameManager.instance.SetAudioBackgroundFade();
        yield return new WaitForSeconds(2);
        if (!_camZoom._flashOn)
        {
            _camZoom.TurnOffFlash();
        }
        _blackAnim.SetTrigger("black");
        yield return new WaitForSeconds(2);
        _television.SetActive(false);
         _hud.SetActive(false);
        _cursor.SetActive(false);
        _monster.SetActive(true);
        _blackAnim.SetTrigger("blinking");
        _swivel.SetTrigger("swivel");
        yield return new WaitForSeconds(8);
        GameManager.instance.LoadNextLevel();

    }

}
