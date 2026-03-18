using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValveInteract : Interactable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private AudioSource _screechSound;

    [SerializeField] private GameObject[] _disappearObjects;

    [SerializeField] private Animator blackAnim;
    [SerializeField] private AudioSource spotlightOff;
    [SerializeField] private AudioSource gaspAudio;

    private bool hasEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {
        base.Interact();
        if (!hasEnded)
        {
            StartCoroutine(EndEverything());
        }


    }

    private IEnumerator EndEverything()
    {
        //first turn it and screech
        hasEnded = true;
        _anim.SetTrigger("turn");
        _screechSound.Play();
        blackAnim.SetTrigger("black");
        //then play the gasp sound
        yield return new WaitForSeconds(1.0f);
        PlayerController.instance._frozen = false;
        foreach (var obj in _disappearObjects) { 
            if(obj != null)
            {
                obj.SetActive(false);
            }
        }
        spotlightOff.Play();
        yield return new WaitForSeconds(1.0f);
       // gaspAudio.Play();
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
