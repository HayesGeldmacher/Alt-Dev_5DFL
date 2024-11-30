using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonSound;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private float _waitTime;
    [SerializeField] private EventSystem _event;

    [SerializeField] private Animator _resetText;
    [SerializeField] private Animator _menuText;
    [SerializeField] private Animator _exitText;
    
    public void CallReset()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Reset());
    }

    public void CallMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Menu());
    }

    public void CallExit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Exit());
    }

    private IEnumerator Menu()
    {
        _buttonSound.Play();
        _blackAnim.SetTrigger("black");
        _resetText.SetTrigger("fade");
        _menuText.SetTrigger("fade");
        _exitText.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(_waitTime);
        _event.ReturnToMenu();
    }

    private IEnumerator Exit()
    {
        _buttonSound.Play();
        _blackAnim.SetTrigger("black");
        _resetText.SetTrigger("fade");
        _menuText.SetTrigger("fade");
        _exitText.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(_waitTime);
        Application.Quit();
    }

    private IEnumerator Reset() {

        _buttonSound.Play();
        _blackAnim.SetTrigger("black");
        _resetText.SetTrigger("fade");
        _menuText.SetTrigger("fade");
        _exitText.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(_waitTime);
        _event.RestartLevel();
    }
}
