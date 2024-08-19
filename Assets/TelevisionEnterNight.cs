using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelevisionEnterNight : Interactable
{

    [SerializeField] private PlayerController _controller;
    [SerializeField] private CameraController _camControl;
    [SerializeField] private Animator _camAnim;
    [SerializeField] private Animator _renderAnim;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private float _decreaseSpeed;
    [SerializeField] private List<GameObject> _lights = new List<GameObject>();

    private float _currentAmbience;
    private int _lines = 0;
    private bool _hasBlackedOut = false;

    private bool _hasStarted = false;
    private bool _decreasing = false;

    
    private void Start()
    {
        _currentAmbience = RenderSettings.ambientIntensity;
    }

   private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _hasStarted)
        {
            _lines += 1;

            if (_lines >= 3)
            {
                if (!_hasBlackedOut)
                {
                    _hasBlackedOut = true;
                    StartCoroutine(StartBlackout());
                }
            }
            else
            {
            base.Interact();
            }
        }

        if (_decreasing)
        {
            _currentAmbience -= 1 * _decreaseSpeed * Time.deltaTime;
            // Debug.Log(_currentAmbience);
            RenderSettings.ambientIntensity = _currentAmbience;

            if(_currentAmbience <= 0)
            {
                _decreasing = false;
            }
        }
    }
    
    
    public override void Interact()
    {
        _renderAnim.SetTrigger("appear");
        _controller._frozen = true;
        _camControl._canInteract = false;
        _hasStarted = true;
        _decreasing = true;
        foreach(GameObject light in _lights)
        {
            light.SetActive(false);
        }
        //StartCoroutine(TVSwoop());

    }


    private IEnumerator StartBlackout()
    {
        _camAnim.SetTrigger("swoop");
        _blackAnim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("HouseReduxNight1");
    }
}
