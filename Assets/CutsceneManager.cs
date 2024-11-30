using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    [SerializeField] private Animator _doorAnim;
    [SerializeField] private Animator _clickAnim;
    private bool _canStart = false;
    private bool _hasStarted = false;
    [SerializeField] private AudioSource _interactSound;
    [SerializeField] private AudioSource _doorSound;

    [Header("Static Audio Fade")]
    [SerializeField] private AudioSource _staticAudio;
    private float _startingVol;
    [SerializeField] private float _fadeSpeed;
    private bool _fading = false;
    private float _currentVol;
     
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(WaitForStart());
        _startingVol = _staticAudio.volume;
        _currentVol = _startingVol;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_hasStarted)
            {
                _hasStarted = true;
                StartSequence();
            }
        }

        if (_fading)
        {
             _currentVol = Mathf.Lerp(_currentVol, 0, _fadeSpeed * Time.deltaTime);
            _staticAudio.volume = _currentVol;
            
        }
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(1.5f);
        if (!_hasStarted)
        {
            _canStart = true;
        }
    }

    private void StartSequence()
    {
        _doorAnim.SetTrigger("start");
        _clickAnim.SetTrigger("click");
        _doorSound.Play();
        _interactSound.Play();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void KillVisualNoise()
    {
        _fading = true;
    }



}
