using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{


    
    public Dialogue dialogue;

    public GameObject _cardToSpawn;

    public bool _canBeClicked = false;
    public Button button;

    [Header("CardSpawn Fields")]
    [SerializeField] private bool _isSpawned;
    [SerializeField] private Animator _cardAnim;
    [SerializeField] private CardGameManager _manager;
    [SerializeField] private int _dialogueClicks;
    public int sentenceNum = 0;
    private bool _interacted = false;


    [SerializeField] private AudioClip _audioClip;


    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetComponent<Button>();
        sentenceNum = 0;
    }

    public void CallOnCreated()
    {
        button = transform.GetComponent<Button>();
        sentenceNum = 0;
        button.enabled = false;
        StartCoroutine(OnCreated());
    }

    public IEnumerator OnCreated()
    {
        _cardAnim.SetTrigger("appear");
        yield return new WaitForSeconds(1f);
        AllowClick();

    }

    public void AllowClick()
    {
        _canBeClicked = true;
        button.enabled = true;
    }

    public void OnSpawnClick()
    {
        if (!_interacted)
        {
            if(_audioClip != null)
            {
                _manager.PlayCardSound(_audioClip);
            }
        }
        
        _interacted = true;
        
        _dialogueClicks -= 1;
        if (_dialogueClicks > 0)
        {
             DisableButton();
            _manager.PlayCardSpawnDialogue(dialogue._sentences[sentenceNum]);
        }
        else
        {
            _manager.PlayCardSpawnDialogue(dialogue._sentences[sentenceNum]);
            _manager.EndCardSpawnDialogue();
            StartCoroutine(DestroyCard());
        }

        sentenceNum += 1;
        Debug.Log("SpawnClicked!!!");

    }


    private IEnumerator DestroyCard()
    {
        button.enabled = false;
        _canBeClicked = false;
        _cardAnim.SetTrigger("disappear");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private IEnumerator DisableButton()
    {
        _canBeClicked = false;
        button.enabled = false;
        yield return new WaitForSeconds(1);
        AllowClick();
    }
}