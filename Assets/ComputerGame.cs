using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerGame : Interactable
{

    [SerializeField] private GameObject _textGame;
    [SerializeField] private Animator _textGameAnim;
    [SerializeField] private TextGameManager _textGameManager;
    private bool _hasExited = false;

    private bool _canInteract = true;

    
    private void Start()
    {
        _textGameManager.enabled = false;
        base.Start();
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
        _textGameManager.StartGame();
        GameManager.instance.FreezePlayer(true);
        _textGame.SetActive(true);

        yield return new WaitForSeconds(1);

        _textGameAnim.SetBool("visible", true);
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


        }
        
    }
}
