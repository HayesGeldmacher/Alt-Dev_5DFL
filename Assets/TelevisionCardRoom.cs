using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelevisionCardRoom : MonoBehaviour
{
    public TelevisionPlayingCard _playingCard;
    public int _currentCard;
    public int[] _animKeys;
    public int[] _dialogueLengths;
    public AudioClip[] _cardSounds;
    public Dialogue[] _dialogues;
    public int _totalCards;

    [Header("Bedroom Fields")]
    public GameObject[] _appearObjects;
    public GameObject[] _disappearObjects;

    
    // Start is called before the first frame update
    void Start()
    {
        _playingCard._key = _animKeys[0];
        _playingCard._dialogue = _dialogues[0];
        _playingCard._totalDialogueNum = _dialogueLengths[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IterateCard()
    {
        _currentCard++;
        if(_currentCard >= _totalCards)
        {
            EnterGameRoom();
        }
        else
        {

            _playingCard._key = _animKeys[_currentCard];
            _playingCard._dialogue = _dialogues[_currentCard];
            _playingCard._totalDialogueNum = _dialogueLengths[_currentCard];
            _playingCard._audio.clip = _cardSounds[_currentCard];
        }
    }

    private void EnterGameRoom()
    {
        foreach(GameObject thing in _appearObjects)
        {
            if(thing != null)
            {
                thing.SetActive(true);
            }
        }

        foreach(GameObject thing in _disappearObjects)
        {
            if(thing != null)
            {
                thing.SetActive(false);
            }
        }
    }
}
