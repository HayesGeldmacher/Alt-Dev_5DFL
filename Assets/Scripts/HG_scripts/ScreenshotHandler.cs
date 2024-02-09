using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
   private static ScreenshotHandler instance;

   private Camera _cam;
   private bool _shouldScreenShot;

    private void Awake()
    {
        instance = this;
        _cam = transform.GetComponent<Camera>();
    }

    WaitForEndOfFrame _frameEnd = new WaitForEndOfFrame();

    //This is where the screenshot and saving actually occur
    private IEnumerator OnPostRender()
    {

        if (_shouldScreenShot)
        {
            yield return _frameEnd;
            _shouldScreenShot = false;
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
           // _render.active = null;
            _cam.targetTexture = null;
        }
    }

    //This is called by the below function, and initiates the screenshot
    private void TakeScreenshot(int _width, int _height)
    {
        _cam.targetTexture = RenderTexture.GetTemporary(_width, _height, 16);
        _shouldScreenShot = true;
        StartCoroutine(OnPostRender());
    }

    //This is the function that we call from other scripts!
    public static void TakeScreenshot_Static(int _width, int _height)
    {
        instance.TakeScreenshot( _width, _height);
    }

}
