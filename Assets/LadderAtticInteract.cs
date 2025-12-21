using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class LadderAtticInteract : Interactable { 


[SerializeField] private Animator _darkAnim;
[SerializeField] private Transform _teleportPosition;
[SerializeField] private Transform _teleportRotation;
[SerializeField] private bool _teleporting = false;
[SerializeField] private float _teleportWait;
[SerializeField] private AudioSource _steps;
[SerializeField] private Transform _playerParent;
[SerializeField] private PlayerController _playerController;
    [SerializeField] private CameraController _camController;
    public float yRotTeleport = 120f;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }


    public override void Interact()
    {
        base.Interact();
        if (!_teleporting)
        {
            StartCoroutine(ClimbLadder());
        }

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }


    private IEnumerator ClimbLadder()
    {
    _teleporting = true;
        _playerController.enabled = false;
    _darkAnim.SetTrigger("black");
    yield return new WaitForSeconds(1f);
       _camController.enabled = false;
     _steps.Play();
    TeleportPlayer();
    yield return new WaitForSeconds(4f);
         _darkAnim.SetTrigger("fadeBack");
        _camController.enabled = true;
        _playerController.enabled = true;
    yield return new WaitForSeconds(1.7f);
    _teleporting = false;
    }

    private void TeleportPlayer()
    {

        //Vector3 relativePos = _teleportRotation.position - _playerParent.position;

        // the second argument, upwards, defaults to Vector3.up
       //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);


        _playerParent.transform.rotation = Quaternion.Euler(_playerParent.transform.rotation.x, yRotTeleport, _playerParent.transform.rotation.z);
        _playerParent.gameObject.SetActive(false);
        Debug.Log("SET THE ROTATION AFTER LADDER");
       _playerParent.position = _teleportPosition.position;
        _playerParent.gameObject.SetActive(true);
}
}
