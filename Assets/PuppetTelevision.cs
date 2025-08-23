using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

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


    [Header("Video Fields")]
    [SerializeField] private Animator _roadAnim;
    [SerializeField] private AudioSource _carDreamAmbience;
    [SerializeField] private Animator _blackFadeIn;
    [SerializeField] private CRT _crt;
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _blackFadeIn.SetTrigger("blackInstant");
        _crt.StartBreathing();
        StartCoroutine(BeginScene());
    }


    private IEnumerator BeginScene()
    {
        yield return new WaitForSeconds(1f);
        _blackFadeIn.SetTrigger("fade");
        yield return new WaitForSeconds(2f);
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

    public void FadeOutPuppet()
    {
        StartCoroutine(StartRoad());
    }

    private IEnumerator StartRoad()
    {
        yield return new WaitForSeconds(3f);
        _roadAnim.SetTrigger("black");
        yield return new WaitForSeconds(3f);
        _roadAnim.SetTrigger("in");
        _carDreamAmbience.Play();
        yield return new WaitForSeconds(16f);
        _roadAnim.SetTrigger("out");
        yield return new WaitForSeconds(4f);
        NextScene();
    }

    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
