using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{
   //private static ScreenshotHandler instance;

   private Camera _cam;
   private bool _shouldScreenShot;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _checkLength;
    [SerializeField] private LayerMask _checkMask;
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private EvidenceManager _evidenceManager;

    [SerializeField] private RawImage _photo;
    [SerializeField] private Texture2D _texture;

    private void Awake()
    {
        //instance = this;
        _cam = transform.GetComponent<Camera>();
        
    }

    WaitForEndOfFrame _frameEnd = new WaitForEndOfFrame();


    private void ScreenShot()
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
    }

    private void CheckForEvidence()
    {
        RaycastHit hit;
        if (Physics.Raycast(_checkPoint.position, transform.forward, out hit, _checkLength,  _checkMask))
        {
            if(hit.transform.tag == "evidence")
            {
            _evidenceManager.PictureTaken(hit.transform.gameObject);
            Debug.Log("Got a object!");

            }
            else if(hit.transform.tag == "ShootTrigger")
            {
                hit.transform.GetComponent<ShootTrigger>().Interact();
            }
            else
            {
                Debug.Log("RAYHIT" + hit.transform.name);
            }
        }

    }

    //This is called by the below function, and initiates the screenshot
    private IEnumerator TakeScreenshot(int _width, int _height)
    {
         yield return _frameEnd;
        ScreenShot();
        CheckForEvidence();
    }

    //This is the function that we call from other scripts!
    public void TakeScreenshot_Static(int _width, int _height)
    {   
       StartCoroutine(TakeScreenshot( _width, _height));       
    }

}
