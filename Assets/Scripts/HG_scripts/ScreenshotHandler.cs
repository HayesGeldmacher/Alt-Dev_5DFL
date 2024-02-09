using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
   //private static ScreenshotHandler instance;

   private Camera _cam;
   private bool _shouldScreenShot;
   public GhostObjects _ghostObject;

    private void Awake()
    {
        //instance = this;
        _cam = transform.GetComponent<Camera>();
        
    }

    WaitForEndOfFrame _frameEnd = new WaitForEndOfFrame(); 

    private void OtherScreenShotMethod()
    {

        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        _cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;
        _cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;
        byte[] _byteArray = renderedTexture.EncodeToPNG();
        string _folderPath = Application.dataPath + "/Screenshots" + "/CameraScreenshot.png";
        System.IO.File.WriteAllBytes(_folderPath, _byteArray);

    }
    
    //This is called by the below function, and initiates the screenshot
    private IEnumerator TakeScreenshot(int _width, int _height)
    {
        yield return _frameEnd;
        OtherScreenShotMethod();
    }

    //This is the function that we call from other scripts!
    public void TakeScreenshot_Static(int _width, int _height, GhostObjects _ghost)
    {   
       StartCoroutine(TakeScreenshot( _width, _height));       
    }

}
