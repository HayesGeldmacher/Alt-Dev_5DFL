using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalChairDreamEnd : Interactable
{

    [Header("Lerp Cam Info")]
    [SerializeField] private Transform _camHolder;
    [SerializeField] private CameraController _camController;
    [SerializeField] private CameraZoom _camZoom;
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private Transform _virtualCam;

    [SerializeField] private Transform _lookPoint;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private bool _lerping = false;
    [SerializeField] private GameObject playerBody;
    private bool _interacted = false;

    [Header("Aim Fields")]
    [SerializeField] private Animator _projectorBackground;
    [SerializeField] private Animator _wallInvisible;
    [SerializeField] private Animator camAnim;
    [SerializeField] private Animator blackOutAnim;

    [Header("Audio Fields")]
    [SerializeField] private AudioSource staticSound;
    [SerializeField] private AudioSource roomToneSound;
    [SerializeField] private float fadeSpeed = 0.05f;
    private bool isFading = false;
    private Quaternion startRotation;
    private Vector3 startPosition;
    float timeElapsed = 0;


    public override void Interact()
    {

        if (!_interacted)
        {
            _interacted = true;
            base.Interact();
            StartCoroutine(EndScene());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_lerping)
        {

            //  Vector3 relativePos = _lookPoint.position - _camHolder.position;
            //  Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            //  _camHolder.position = Vector3.Lerp(_camHolder.position, _anchorPoint.position, _lerpSpeed * Time.deltaTime);
            // _camHolder.rotation = Quaternion.Lerp(_camHolder.rotation, rotation, _lerpSpeed * Time.deltaTime);

           // timeElapsed += 1 * Time.deltaTime * _lerpSpeed;
            _camHolder.position = Vector3.Lerp(_camHolder.position, _anchorPoint.position,      1 * Time.deltaTime);
            _camHolder.rotation = Quaternion.Lerp(_camHolder.rotation, _lookPoint.rotation, 1 * Time.deltaTime);
            _virtualCam.transform.localRotation = Quaternion.Lerp(_virtualCam.localRotation, Quaternion.Euler(0, 0, 0), 1 * Time.deltaTime);
            //Quaternion.Lerp(_virtualCam.rotation, zeroRot, 1 * Time.deltaTime);
        }

        if (isFading)
        {
            float currentVolume = staticSound.volume;
            float newVolume = currentVolume -= (fadeSpeed * Time.deltaTime);
            staticSound.volume = newVolume;
 

            currentVolume = roomToneSound.volume;
            newVolume = currentVolume -= (fadeSpeed * Time.deltaTime);
            roomToneSound.volume = newVolume;

            if (roomToneSound.volume <= 0 && staticSound.volume <= 0) {

                isFading = false;
            }

        }
    }

    private IEnumerator EndScene()
    {
        _camHolder.parent = null;
        _camHolder.parent = transform;
        _camZoom._canZoom = false;
        yield return new WaitForSeconds(0.1f);
        //Destroy(_camController);

        _camController._frozen = true;
        Destroy(playerBody);
        PlayerController.instance.enabled = false;
        _lerping = true;
        yield return new WaitForSeconds(4f);
        Destroy(_camZoom);
        yield return new WaitForSeconds(1f);
        camAnim.SetTrigger("zoom");
        yield return new WaitForSeconds(10f);
        isFading = true;
        camAnim.SetTrigger("sleep");
        blackOutAnim.SetTrigger("sleep");
       // _projectorBackground.SetTrigger("fade");
       // _wallInvisible.SetTrigger("fade");
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
