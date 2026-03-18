using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadFinalManager : MonoBehaviour
{
    public Animator screenAnim;
    public float screenWatchTime;
    public float videoWatchTime;
    public AudioSource screenAudio;
    public AudioSource vidAudio;
    public AudioFadeIn audioFadeIn;
    public AudioFadeOut audioFadeOut;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(4f);
        screenAnim.SetTrigger("start");
        audioFadeIn.StartFading();
        yield return new WaitForSeconds(8f);
        audioFadeIn.StopFading();
        audioFadeOut.StartFading();
        yield return new WaitForSeconds(11f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
