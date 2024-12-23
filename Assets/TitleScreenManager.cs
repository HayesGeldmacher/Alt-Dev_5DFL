using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

    private bool _startedTransition = false;
    [SerializeField] private Animator _blackOutAnim;
    [SerializeField] private AudioSource _interactAudio;
    [SerializeField] private Transform _cursorSprite;
    [SerializeField] TitleScreenSpriteFollowMouse _spriteFollow;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _spriteFollow.EnableCursor(true);
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
        _cursorSprite.SetParent(null);
        _cursorSprite.GetComponent<Animator>().SetTrigger("fade");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        Cursor.visible = false;
        _cursorSprite.SetParent(null);
        _cursorSprite.GetComponent<Animator>().SetTrigger("fade");
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
