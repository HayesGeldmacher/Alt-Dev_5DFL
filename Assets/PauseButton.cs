using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonSound;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private float _waitTime;
    [SerializeField] private EventSystem _event;

    [SerializeField] private Animator _resetText;
    
    public void CallReset()
    {
        StartCoroutine(Reset());    
    }

    private IEnumerator Reset() {

        _buttonSound.Play();
        _blackAnim.SetTrigger("black");
        _resetText.SetTrigger("fade");
        yield return new WaitForSecondsRealtime(_waitTime);
        _event.RestartLevel();
       // Debug.Log("FUCK!");

    }
}
