using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PuppetCutsceneManager : MonoBehaviour
{
    [SerializeField] private AudioSource birthdaySound;
    [SerializeField] private Animator screenAnim;
    [SerializeField] private float puppetWatchTime;
    [SerializeField] private bool fadingUp = false;
    [SerializeField] private bool fadingDown = false;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private VideoPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(2f);
        screenAnim.SetTrigger("enter");
        player.Play();
        birthdaySound.Play();
        fadingUp = true;
        yield return new WaitForSeconds(puppetWatchTime);
        fadingDown = true;
        screenAnim.SetTrigger("exit");
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }


    // Update is called once per frame
    void Update()
    {
        float currentVolume = birthdaySound.volume;
        float newVolume = currentVolume;
        if (fadingUp)
        {
           newVolume = currentVolume + Time.deltaTime * fadeSpeed;
           if(newVolume >= 1)
            {
                fadingUp = false;
            }
        }
        else if (fadingDown)
        {
           newVolume = currentVolume - Time.deltaTime * fadeSpeed;
            if( newVolume <= 0)
            {
                fadingDown = false;
            }
        }

        birthdaySound.volume = newVolume;
    }
}
