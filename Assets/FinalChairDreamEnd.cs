using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalChairDreamEnd : Interactable
{

    [Header("Lerp Cam Info")]
    [SerializeField] private Transform _camHolder;
    [SerializeField] private CameraController _camController;
    [SerializeField] private Transform _anchorPoint;
    [SerializeField] private Transform _lookPoint;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private bool _lerping = false;
    private bool _interacted = false;

    [Header("Aim Fields")]
    [SerializeField] private Animator _projectorBackground;
    [SerializeField] private Animator _wallInvisible;





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
            _camHolder.localPosition = Vector3.Lerp(_camHolder.localPosition, _anchorPoint.localPosition, _lerpSpeed * Time.deltaTime);
            _camHolder.localRotation = Quaternion.Lerp(_camHolder.localRotation, _lookPoint.localRotation, _lerpSpeed * Time.deltaTime);
        }
    }

    private IEnumerator EndScene()
    {
        _camHolder.parent = null;
        _camHolder.parent = transform;
        yield return new WaitForSeconds(0.1f);
        Destroy(_camController);
         Destroy(PlayerController.instance.transform.gameObject);
        _lerping = true;
        yield return new WaitForSeconds(10f);
        _projectorBackground.SetTrigger("fade");
        _wallInvisible.SetTrigger("fade");
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
