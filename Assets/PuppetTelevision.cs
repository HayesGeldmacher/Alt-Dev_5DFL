using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PuppetTelevision : MonoBehaviour
{

    [Header("Object Fields")]
    public GameObject _puppet1;
    public GameObject _puppetVideo;
    public GameObject _CRTVideo;
    public VideoPlayer _player;

    [Header("Wait Puppet 1 Start")]
    public float _introWait;

    [Header("Wait video start")]
    public float _videoStartWait;
    private bool _startedVid = false;
    private bool _finishedVid = false;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPuppetFirst());
        StartCoroutine(StartVideoWait());
    }


    public IEnumerator StartVideoWait()
    {
        yield return new WaitForSeconds(_videoStartWait);
        StartVideo();
        yield return new WaitForSeconds(5.5f);
        StopVideo();
    }


    public IEnumerator StartPuppetFirst()
    {
        yield return new WaitForSeconds(_introWait);
        _puppet1.SetActive(true);
        _puppet1.GetComponent<Animator>().SetTrigger("fly");
    }


    public void StartVideo()
    {

        _CRTVideo.SetActive(false);
        _startedVid = true;
        _player.Play();
        
    }

    public void StopVideo()
    {
        _CRTVideo.SetActive(true);
    }
}
