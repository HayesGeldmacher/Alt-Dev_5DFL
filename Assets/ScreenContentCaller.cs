using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenContentCaller : MonoBehaviour
{

    [SerializeField] private TextGameManager _textGame;
    [SerializeField] private AudioSource _audioScare;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallEndInput()
    {
        _textGame.SetEndInput();
    }

    public void CallEndScare()
    {
        _textGame.EndScare();
    }

    public void ScareSound()
    {
        _audioScare.Play();
    }
}
