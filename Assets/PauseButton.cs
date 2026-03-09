using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    private bool inSettings = false;

    public GameObject menuButtonsParent;

    public GameObject settingsButtons;
    public GameObject settingsText;
    public SettingsAssign settingsAssign;

    public SettingsMenu menu;
    public Animator settingsBlack;

    public void OnEscape()
    {
        if (inSettings)
        {
            StartCoroutine(LeaveSettings());
        }
    }

    private IEnumerator Escape()
    {
        inSettings = false;
        _buttonSound.Play();
     
        settingsBlack.SetTrigger("fadeOut");
        settingsAssign.AssignPlayerPreferences();
        yield return new WaitForSecondsRealtime(0.5f);
        settingsBlack.SetTrigger("fadeIn");
        menuButtonsParent.SetActive(true);
        settingsText.SetActive(false);
        settingsButtons.SetActive(false);
        _resetText.SetTrigger("appear");
        _menuText.SetTrigger("appear");
        _exitText.SetTrigger("appear");
    }

    //disables the cursor without actually doing any functionality
    public void DisableCursor()
    {
        _pauseCursor.EnableCursor(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
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

    public void CallSettings()
    {
        if (!inSettings)
        {
            StartCoroutine(Settings());

        }
    }

    private IEnumerator Settings()
    {
        inSettings = true;
        _buttonSound.Play();
       // _blackAnim.SetTrigger("black");
        _resetText.SetTrigger("fade");
        _menuText.SetTrigger("fade");
        _exitText.SetTrigger("fade");
        settingsBlack.SetTrigger("fadeIn");
        yield return new WaitForSecondsRealtime(0.5f);
        menuButtonsParent.SetActive(false);
        settingsButtons.SetActive(true);
        settingsText.SetActive(true);
        menu.AssignPreferences();

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

    public void CallLeaveSettings()
    {
        if (inSettings)
        {
            StartCoroutine(LeaveSettings());
        }
    }

    public IEnumerator LeaveSettings()
    {
        inSettings = false;
        _buttonSound.Play();
        // _blackAnim.SetTrigger("black");
        settingsBlack.SetTrigger("fadeOut");
        settingsAssign.AssignPlayerPreferences();
        yield return new WaitForSecondsRealtime(0.5f);
        settingsBlack.SetTrigger("fadeIn");
        menuButtonsParent.SetActive(true);
        settingsText.SetActive(false);
        settingsButtons.SetActive(false);
        _resetText.SetTrigger("appear");
        _menuText.SetTrigger("appear");
        _exitText.SetTrigger("appear");
    }

    
}
