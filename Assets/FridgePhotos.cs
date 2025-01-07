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

        newsprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(0.5f, 0.5f));

        Vector3 oldScale = _photos[_currentPhoto].gameObject.transform.localScale;
        Vector3 newScale = new Vector3(oldScale.x /5, oldScale.y /5);  
        _photos[_currentPhoto].gameObject.transform.localScale = newScale;
        _photos[_currentPhoto].sprite = newsprite;
        _photos[_currentPhoto].color = new Vector4(255, 255, 255, 255);  
        _currentPhoto++;

        Debug.Log("Changed photos!");

    }
}
