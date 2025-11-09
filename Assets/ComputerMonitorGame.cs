using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMonitorGame : Interactable
{
    [SerializeField] private GameObject _textGame;
    [SerializeField] private Animator _textGameAnim;
    public CardGameManager _cardGameManager;
    [SerializeField] private TitleScreenSpriteFollowMouse _mouseCursorText;
    [SerializeField] private GameObject _mouseCursorInteract;
    [SerializeField] private GameObject _buttonParent;
    [SerializeField] private GameObject _pauseButtonMaster;
    private bool _hasExited = false;

    private bool _canInteract = true;



    private void Start()
    {
        base.Start();
        _buttonParent.SetActive(false);
        _cardGameManager.enabled = false;
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
        _cardGameManager.enabled = true;
        _pauseButtonMaster.SetActive(false);
        _cardGameManager.StartGame();
        GameManager.instance.FreezePlayer(true);
        _textGame.SetActive(true);
        GameManager.instance._inTextGame = true;
        _mouseCursorInteract.SetActive(false);
        yield return new WaitForSeconds(1);
        _textGameAnim.SetBool("visible", true);
        yield return new WaitForSeconds(1.5f);
        _mouseCursorText.EnableCursor(true);
        _mouseCursorText.EnterTextGame(true);
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
            _mouseCursorText.EnterTextGame(false);
            GameManager.instance.FreezePlayer(false);
            _textGame.SetActive(false);
            _mouseCursorInteract.SetActive(true);
            Debug.Log("ENDEDTEXTGAME!!");


        }

    }
}
