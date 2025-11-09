using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopeNightInteractable : Interactable
{
    [SerializeField] private Animator _houseAnim;
    [SerializeField] private bool _startedInteraction = false;
    [SerializeField] private float _interactWait;
    private bool _canInteract = false;
    [SerializeField] private Animator _cursorAnim;

    private BoxCollider _boxCollider;
    [SerializeField] private AudioSource _muffledAudio;

    [SerializeField] private AudioSource _interactAudio;
    [SerializeField] private CameraController _controller;

    [Header("Disappear Objects")]
    [SerializeField] private GameObject[] disappearObjects;
    [SerializeField] private GameObject[] appearObjects;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _boxCollider = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (_startedInteraction)
        {
            if (_canInteract)
            {
                if (Input.GetButtonDown("Interact"))
                {

                    _interactAudio.Play();
                    _canInteract = false;
                    _cursorAnim.SetTrigger("click");
                    _cursorAnim.SetBool("appear", false);
                    StartCoroutine(EndInteraction());
                }
            }
        }
    }

    public override void Interact()
    {

        if (!_startedInteraction)
        {
            _interactAudio.Play();
            _startedInteraction = true;
            _boxCollider.enabled = false;
            StartCoroutine(StartInteraction());
        }
    }

    private IEnumerator StartInteraction()
    {
        _muffledAudio.Play();
        PlayerController.instance._frozen = true;
        _controller._frozen = true;
        _houseAnim.SetTrigger("startHouse");
        yield return new WaitForSeconds(7f);
        base.Interact();
        yield return new WaitForSeconds(1.5f);
        _canInteract = true;
        _cursorAnim.SetBool("appear", true);

    }

    private IEnumerator EndInteraction()
    {
        base.Interact();
        _houseAnim.SetTrigger("endHouse");
        yield return new WaitForSeconds(8);
        PlayerController.instance._frozen = false;
        _controller._frozen = false;
        foreach(GameObject thing in appearObjects)
        {
            thing.SetActive(true);
        }

        foreach(GameObject thing in disappearObjects)
        {
            thing.SetActive(false);
        }

        transform.GetComponent<MeshRenderer>().enabled = false;
    }

}
