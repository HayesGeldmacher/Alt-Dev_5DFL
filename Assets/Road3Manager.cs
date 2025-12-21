using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Road3Manager : MonoBehaviour
{

    public Animator screenAnim;
    public Animator videoAnim;
    public float screenWatchTime;
    public float videoWatchTime;
    public AudioSource screenAudio;
    public AudioSource vidAudio;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(4f);
        screenAnim.SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        screenAudio.Play();
        yield return new WaitForSeconds(screenWatchTime);
        screenAnim.SetTrigger("end");
        yield return new WaitForSeconds(8f);
        videoAnim.SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        vidAudio.Play();
        yield return new WaitForSeconds(videoWatchTime);
        videoAnim.SetTrigger("end");
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
