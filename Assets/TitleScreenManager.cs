using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

    private bool _startedTransition = false;
    [SerializeField] private Animator _blackOutAnim;
    [SerializeField] private AudioSource _interactAudio;
    [SerializeField] private AudioSource _staticAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void CallStartGame()
    {
        if (!_startedTransition)
        {
            _startedTransition = true;
            _interactAudio.Play();
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        _blackOutAnim.SetTrigger("fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ComputerGamePrologue");
    }

    public void CallExitGame()
    {
        if (!_startedTransition)
        {
            _startedTransition = true;
            _interactAudio.Play();
            StartCoroutine(ExitGame());
        }
    }

    private IEnumerator ExitGame()
    {
        _blackOutAnim.SetTrigger("fade");
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
