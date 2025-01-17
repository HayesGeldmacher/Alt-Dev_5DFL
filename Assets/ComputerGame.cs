using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComputerGame : Interactable
{

    [SerializeField] private GameObject _textGame;
    [SerializeField] private Animator _textGameAnim;
    [SerializeField] private TextGameManager _textGameManager;
    [SerializeField] private GameObject _mouseCursorText;
    [SerializeField] private GameObject _mouseCursorInteract;
    [SerializeField] private GameObject _buttonParent;
    [SerializeField] private GameObject _pauseButtonMaster;
    private bool _hasExited = false;

    private bool _canInteract = true;
    [SerializeField] private TitleScreenSpriteFollowMouse _screenCursor;

    
    private void Start()
    {
        _textGameManager.enabled = false;
        _mouseCursorText.SetActive(false);
        base.Start();
        _buttonParent.SetActive(false);
    }

    private void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        if (_canInteract)
        {
            _canInteract = false;
            base.Interact();
            StartCoroutine(EnterGame());

        }

    }

    private IEnumerator EnterGame()
    {
        _textGameManager.enabled = true;
        _pauseButtonMaster.SetActive(false);
        _textGameManager.StartGame();
        GameManager.instance.FreezePlayer(true);
        _textGame.SetActive(true);
        GameManager.instance._inTextGame = true;
        _mouseCursorInteract.SetActive(false);
        yield return new WaitForSeconds(1);
        _textGameAnim.SetBool("visible", true);
        yield return new WaitForSeconds(1.5f);
        _screenCursor.EnableCursor(true);
        _mouseCursorText.SetActive(true);
        _buttonParent.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private IEnumerator ExitGame()
    {
        if (!_hasExited)
        {
            _hasExited = true;

        _textGameAnim.SetBool("visible", false);
        yield return new WaitForSeconds(1);

        GameManager.instance.FreezePlayer(false);
        _textGame.SetActive(false);
            _mouseCursorInteract.SetActive(true);


        }
        
    }
}
