using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleManager : MonoBehaviour
{
    [Header("Scene Timing Fields")]
    public float _introWait; //how long to fade in, should have audio immediately but fade in over seconds
    public float _openTime; //how long to stay open before fade again
    public float _closeTime; //how long after start fading to end game, go to credits

    public Animator _eyesAnim; //animator for eyes slowly close and fade away
    public AudioSource _ambientAudio; //ambience, people talking, et cetera
    //add something here for camera can move limited!
    

    private bool _started = false;
    private bool _ended = false;
    
    //scene lasts for 60 seconds, and then slowly fades out!
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (_started)
        {
            if (!_ended)
            {
                _openTime -= Time.deltaTime;
                if (_openTime <= 0)
                {
                    _ended = true;
                    StartCoroutine(EndScene());
                }
            }
        }
    }

    private IEnumerator BeginScene()
    {
        yield return new WaitForSeconds(_introWait);
        _eyesAnim.SetTrigger("open");
        StartAudio();
        _started = true;
        //allow cam to move limited
    }

    private IEnumerator EndScene()
    {
        //stop cam from moving
        EndAudio();
        _eyesAnim.SetTrigger("close");
        yield return new WaitForSeconds(_closeTime);
        //end game, go to credits!1

    }


    private void StartAudio()
    {

    }

    private void EndAudio()
    {

    }
}
