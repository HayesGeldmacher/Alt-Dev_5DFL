using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerGame : Interactable
{

    [SerializeField] private GameObject _textGame;
    [SerializeField] private Animator _textGameAnim;

    private bool _canInteract = true;

    
    private void Start()
    {
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
        GameManager.instance.FreezePlayer(true);
        _textGame.SetActive(true);

        yield return new WaitForSeconds(1);

        _textGameAnim.SetBool("visible", true);
    }

    private IEnumerator ExitGame()
    {
        _textGameAnim.SetBool("visible", false);

        yield return new WaitForSeconds(1);

        GameManager.instance.FreezePlayer(false);
        _textGame.SetActive(false);
    }
}
