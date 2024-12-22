using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    [SerializeField] private TitleScreenSpriteFollowMouse _pauseCursor;
    
    public void CallReset()
    {
        _pauseCursor.EnableCursor(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Reset());
    }

    public void CallMenu()
    {
        _pauseCursor.EnableCursor(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(Menu());
    }

    public void CallExit()
    {
        _pauseCursor.EnableCursor(false);
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
        GameManager.instance.LoadMenu();
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
        GameManager.instance.ReloadLevel();
    }
}
