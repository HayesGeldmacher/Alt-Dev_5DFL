using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FridgePhotos : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _photos;
    [SerializeField] private int _currentPhoto = 0;
    [SerializeField] private int _maxPhotos = 5;



    public void ChangePhoto(Texture2D screenShot)
    {
        if (_currentPhoto >= _maxPhotos) return;


        //Texture2D newScreenShot = screenShot;

        // newScreenShot.Reinitialize(screenShot.width/5, screenShot.height/5);
        //newScreenShot.Apply();

        Sprite newsprite;

        float newWidth = Screen.width / 200;
        float newHeight = Screen.height / 200;
        //get rid of resize here, and instead change localScale multiplier to deal with the gap!
       // newsprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width / newWidth, screenShot.height / newHeight), new Vector2(0.5f, 0.5f));
        newsprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(0.5f, 0.5f));
        Debug.Log("SCREENSHOT WIDTH: " + screenShot.width);
        Debug.Log("SCREENSHOT HEIGHT: " + screenShot.height);
        Vector3 oldScale = _photos[_currentPhoto].gameObject.transform.localScale;
        //Vector3 newScale = new Vector3(oldScale.x, oldScale.y);
        Vector3 newScale = new Vector3(oldScale.x / newWidth, oldScale.y / newHeight);
        _photos[_currentPhoto].gameObject.transform.localScale = newScale;
        _photos[_currentPhoto].sprite = newsprite;
        _photos[_currentPhoto].color = new Vector4(255, 255, 255, 255);  
        _currentPhoto++;

        Debug.Log("Changed photos!");

    }
}
