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

    private void OnPostRender()
    {

        if (_shouldScreenShot)
        {
            _shouldScreenShot = false;
            RenderTexture _render = _cam.targetTexture;

            Texture2D _renderResult = new Texture2D(_render.width, _render.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, _render.width, _render.height);
            _renderResult.ReadPixels(rect, 0, 0);

            byte[] _byteArray = _renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", _byteArray);
            Debug.Log("Saved Screenshot!");

            RenderTexture.ReleaseTemporary(_render);
           // _render.active = null;
            _cam.targetTexture = null;
        }
    }

    private void TakeScreenshot(int _width, int _height)
    {
        _cam.targetTexture = RenderTexture.GetTemporary(_width, _height, 16);
        _shouldScreenShot = true;
        OnPostRender();
    }

    public static void TakeScreenshot_Static(int _width, int _height)
    {
        instance.TakeScreenshot( _width, _height);
    }

}
