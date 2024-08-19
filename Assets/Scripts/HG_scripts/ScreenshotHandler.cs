using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{
    //private static ScreenshotHandler instance;

    private Camera _cam;
    private bool _shouldScreenShot;
    [SerializeField] private Camera _monsterCam;

    [SerializeField] private float _checkRadius;
    [SerializeField] private float _checkLength;
    [SerializeField] private LayerMask _checkMask;
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private EvidenceManager _evidenceManager;

    [SerializeField] private RawImage _photo;
    [SerializeField] private Animator _photoAnim;
    [SerializeField] private Animator _iconAnim;
    [SerializeField] private AudioSource _ding;
    [SerializeField] private AudioSource _chime;
    [SerializeField] private GameManager _manager;
    [SerializeField] private CameraZoom _zoom;
    [SerializeField] private CameraController _camControl;
    [SerializeField] private Datamosh _data;

    [SerializeField] private bool _canOpenPhoto = true;
    [HideInInspector] public bool _hasPhoto = false;
    [HideInInspector] public bool _photoOpen = false;
    [SerializeField] private AudioSource _negativeSound;

    [HideInInspector] public bool _showPhoto = true;
    [SerializeField] private MonsterFace _monsterFace;


    private void Awake()
    {
        //instance = this;
        _cam = transform.GetComponent<Camera>();
        _showPhoto = true;
        
    }

    WaitForEndOfFrame _frameEnd = new WaitForEndOfFrame();

    private void Update()
    {
       
        if (_manager._isPaused) return;
        if (_zoom._isZooming) return;
        if (_camControl._interacting) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_hasPhoto)
            {

                if (_photoOpen)
                {
                    if (_showPhoto)
                    {
                         _photoOpen = false;
                        _photoAnim.SetTrigger("fade");
                        StartCoroutine(IconBump());
                    }
                    else
                    {
                        PlayNegativeSound();
                    }
                }
                else
                {
                    _photoOpen = true;
                    PlayDing();
                    _photoAnim.SetTrigger("appear");
                }
            
            }
            else
            {
                PlayNegativeSound();
            }



        }
    }


    private IEnumerator IconBump()
    {
        _canOpenPhoto = true;
        PlayDing();
        yield return new WaitForSeconds(0.1f);
        if (_photoOpen)
        {
        _iconAnim.SetTrigger("disappear");

        }
        else
        {
            _iconAnim.SetTrigger("appear");
        }

    }

    private void IconDisappear()
    {
        _canOpenPhoto = false;
        _iconAnim.SetTrigger("disappear");
        if (_hasPhoto)
        {
        _photoAnim.SetTrigger("disappear");

        }
    }

    public void CallSetMonster()
    {
       //_showPhoto = false;
       StartCoroutine(SetMonsterScreen());
    }

    private IEnumerator SetMonsterScreen()
    {
        yield return new WaitForSeconds(1f);
        _showPhoto = false;
        Debug.Log("STARTEDMONSTER1!");
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        _monsterCam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        _cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();

        Texture2D newScreenshot = new Texture2D(renderedTexture.width, renderedTexture.height, TextureFormat.RGB24, false);
        newScreenshot.SetPixels(renderedTexture.GetPixels());
        newScreenshot.Apply();

        Destroy(screenshot);

        _photo.texture =_monsterCam.targetTexture;

        _hasPhoto = true;

        yield return new WaitForSeconds(0.4f);
        //_photoAnim.SetTrigger("fade");
        yield return new WaitForSeconds(0.1f);
        //_iconAnim.SetTrigger("appear");

        _monsterFace.CallStartTalking();
    }

    private IEnumerator ScreenShot()
    {

        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        _cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        _cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;

        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();

        Texture2D newScreenshot = new Texture2D(renderedTexture.width, renderedTexture.height, TextureFormat.RGB24, false);
        newScreenshot.SetPixels(renderedTexture.GetPixels());
        newScreenshot.Apply();

        Destroy(screenshot);

        _photo.texture = newScreenshot;
            
        _hasPhoto = true;

        yield return new WaitForSeconds(0.4f);
        _photoAnim.SetTrigger("fade");
        yield return new WaitForSeconds(0.1f);
        _iconAnim.SetTrigger("appear");
        
    }

    private void CheckForEvidence()
    {
        RaycastHit hit;
        if (Physics.Raycast(_checkPoint.position, transform.forward, out hit, _checkLength,  _checkMask))
        {
            if(hit.transform.tag == "evidence")
            {
            _evidenceManager.PictureTaken(hit.transform.gameObject);
                _evidenceManager.StrikeOffItem(hit.transform.gameObject);
            Debug.Log("Got a object!");
                StartCoroutine(EvidenceDing());

            }
            else
            {
                Debug.Log("RAYHIT" + hit.transform.name);

            }

            if (hit.transform.tag == "ShootTrigger")
            {
                hit.transform.GetComponent<ShootTrigger>().Interact();
            }
            else
            {
                Debug.Log("RAYHIT" + hit.transform.name);
               
            }
        }

         StartCoroutine(ScreenShot());
    
    }

    //This is called by the below function, and initiates the screenshot
    private IEnumerator TakeScreenshot(int _width, int _height)
    {
         yield return _frameEnd;
        CheckForEvidence();
    }

    //This is the function that we call from other scripts!
    public void TakeScreenshot_Static(int _width, int _height)
    {   
       StartCoroutine(TakeScreenshot( _width, _height));       
    }


    private void PlayDing()
    {
        
        _ding.pitch = Random.Range(0.8f, 1.1f);
        _ding.Play();
    }

    public void CallEvidenceDing()
    {
        StartCoroutine(EvidenceDing());
    }

    private IEnumerator EvidenceDing()
    {

        _data.Glitch();
        _data.Glitch();
       
        yield return new WaitForSeconds(1.2f);
        _chime.pitch = Random.Range(0.8f, 1.1f);
        _chime.Play();
    }

    private void PlayNegativeSound()
    {
        _negativeSound.pitch = Random.Range(0.8f, 1.1f);
        _negativeSound.Play();
    }
}
