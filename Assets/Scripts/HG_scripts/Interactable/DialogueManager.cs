using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> _sentences;
    public TMP_Text _dialogueText;
    private Interactable _currentTrigger;
    [SerializeField ]private Animator _textAnim;
    [SerializeField] private PlayerController _controller;

    private void Start()
    {
        _sentences = new Queue<string>();
    }

    //Begins a conversation when the dialogue trigger is activated from an NPC or item
    public void StartDialogue(Dialogue _dialogue, Interactable _trigger)
    {
        _currentTrigger = _trigger;
        
        _textAnim.SetBool("active", true);

        _sentences.Clear();

        //uses FIFO to queue up each sentence in our public dialogue box
        foreach(string sentence in _dialogue._sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(_sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        //Takes the next-added sentence out of the queue and loads it into the text box
        string _loadedText = _sentences.Dequeue();
        _dialogueText.text = _loadedText;
        Debug.Log(_loadedText);
    }

    public void EndDialogue()
    {
        Debug.Log("End of Conversation");
        _textAnim.SetBool("active", false);
        _dialogueText.text = "";
       
        if (_currentTrigger)
        {
            _currentTrigger._startedTalking = false;
        }
    }

    public void CallTimerEnd(float _textTime)
    {
        StartCoroutine(TimerEnd(_textTime));
    }

    private IEnumerator TimerEnd(float _textTime)
    {
        yield return new WaitForSeconds(_textTime);
        EndDialogue();
    }
}
