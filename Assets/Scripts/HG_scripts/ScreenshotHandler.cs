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

    //This is where the screenshot and saving actually occur
    private IEnumerator OnPostRender()
    {

        if (_shouldScreenShot)
        {
            _shouldScreenShot = false;
            yield return _frameEnd;
          

            

            RenderTexture _render = _cam.targetTexture;

            Texture2D _renderResult = new Texture2D(_render.width, _render.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, _render.width, _render.height);
            _renderResult.ReadPixels(rect, 0, 0);

            byte[] _byteArray = _renderResult.EncodeToPNG();
            //string _folderPath = Application.dataPath + "/CameraScreenshot.png";
            string _folderPath = Application.dataPath + "/Screenshots" +  "/CameraScreenshot.png";
            System.IO.File.WriteAllBytes(_folderPath, _byteArray);
            Debug.Log("Saved Screenshot!");

            RenderTexture.ReleaseTemporary(_render);
           
            _cam.targetTexture = null;

            if(_ghostObject != null)
            {
                _ghostObject.Disappear();
            }
        }
    }

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
        private void TakeScreenshot(int _width, int _height)
    {
        //_cam.targetTexture = RenderTexture.GetTemporary(_width, _height, 16);
        _shouldScreenShot = true;
            OtherScreenShotMethod();
    }

    //This is the function that we call from other scripts!
    public void TakeScreenshot_Static(int _width, int _height, GhostObjects _ghost)
    {
        
        TakeScreenshot( _width, _height);
         
    }

}
