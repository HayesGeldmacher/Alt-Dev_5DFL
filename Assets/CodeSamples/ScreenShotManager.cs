using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System.Runtime.CompilerServices;


[RequireComponent(typeof(Camera))]
public class ScreenshotManager : MonoBehaviour
{

    ///This script handles the taking of pictures, including: 
    /// Raycasting to objects in the scene to determine if important objects were shot
    /// Rendering the camera viewport to a 2D texture and saving for later viewing
    /// Managing light animations and sounds related to the picture-taking 

    [Header("Raycast Picture Check Variables")]
    
    //Determines how far away the player can take a picture of a key item
    [SerializeField] private float _checkLength;

    //Determines how accurate the player must be when shooting a key item
    [SerializeField] private float _checkRadius;

    //Determines what GameObjects the picture check ray can be casted to
    [SerializeField] private LayerMask _checkMask;

    //The picture check ray will be casted from this transform's location
    [SerializeField] private Transform _checkPoint;

    [Header("Image Render Variables")]

    //The UI RawImage that rendered screenshots are saved to
    [SerializeField] private RawImage _photo;

    //The camera that the image will be rendered from
    private Camera _cam;

    //Accessed when taking picture of key evidence items
    [SerializeField] private EvidenceManager _evidenceManager;

    [Header("Illumination Variables")]
    //Determines whether average illumuination should be considered when raycasting to evidence
    [SerializeField] private bool _requiresIllumination = true;
    [SerializeField] private float _minLuminance;

    //Other scripts need to know whether the player currently has a photo saved and whether it is open
    [HideInInspector] public bool _hasPhoto = false;
    [HideInInspector] public bool _photoOpen = false;

    [Header("Visual Effects")]

    //Calls the datamoshing effect that occurs when taking picture of a key item
    [SerializeField] private Datamosh _data;

    //Handles all animations for the image renders and HUD
    //[SerializeField] private ImageAnimationManager _imageAnimationManager;

    //Post process volume should only be enabled for the duration of the screenshot for increased performance
    [SerializeField] private PostProcessLayer _volume;

    //Manager only plays 3 audio clips, no need to place them in array
    [Header("Audio Variables")]
    [SerializeField] private AudioClip _photoIconDing;
    [SerializeField] private AudioClip _evidenceChime;
    [SerializeField] private AudioClip _noLightSound;

    private AudioSource _audio;
    private void Awake()
    {
        _cam = transform.GetComponent<Camera>();
       
        //Disabling the post-process volume until render time for optimization
        _volume.enabled = false;
    }

    //Setting a WaitForEndOfFrame so that screenshots wait until frame has fully rendered beforoe recording
    WaitForEndOfFrame _frameEnd = new WaitForEndOfFrame();

    //Called from other scripts 
    public void TakeScreenshot_Static(int width, int height)
    {
       StartCoroutine(TakeScreenshot(width, height));
    }

    //Called by above function and initiates the screenshot
    private IEnumerator TakeScreenshot(int width, int height)
    {
        //Waiting for the end of the frame so that the screen has completed rendering
        yield return _frameEnd;

        //First render the camera screen to a 2D texture 
        Texture2D screenshot = GetCameraRender();

        // Determine whether the screenshot is suitably illuminated
        bool illuminated = IsIlluminated(screenshot);

        // If illumination is required and the screenshot is not suitably illuminated, return early
        if (_requiresIllumination && !illuminated)
        {
            //Play the no-light audio clip
            PlaySound(_noLightSound);
        }

        // After passing checks, we can update the photo
        _photo.texture = screenshot;
        _hasPhoto = true;

        CheckForEvidence();
    }
    
    private void CheckForEvidence()
    {
        //Casting a ray from the player's camera to check if it collides with evidence
        RaycastHit hit;
        if (Physics.Raycast(_checkPoint.position, transform.forward, out hit, _checkLength,  _checkMask))
        {
            //Checking if the RaycasHit has collided with an evidence object
            if(hit.transform.tag == "evidence")
            {
                //Calling evidence manager to update level progression
                _evidenceManager.PictureTaken(hit.transform.gameObject);
                Debug.Log("EVIDENCE: " + hit.transform.name);
                StartCoroutine(EvidenceDing());     
            }
    
            //If not evidence, check if RaycastHit collided with event trigger
            else if (hit.transform.tag == "ShootTrigger")
            {
                if (hit.transform.TryGetComponent<ShootTrigger>(out ShootTrigger trigger))
                {
                    trigger.Interact();
                    Debug.Log("SHOOT TRIGGER: " + hit.transform.name);
                }
            }

            //if not colliding with a trigger, check object collision
            else
            {
                Debug.Log("RAYHIT: " + hit.transform.name);
            }
        }
    }

    private Texture2D GetCameraRender()
    {
        
        //Enabling a post-process volume only right before we render the camera
        _volume.enabled = true;

        //Creating a new render texture as a blank canvas on which the screenshot will be projected
        RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
        _cam.targetTexture = screenTexture;
        RenderTexture.active = screenTexture;

        //Rendering whatever the camera sees in its viewport
        _cam.Render();
        Texture2D renderedTexture = new Texture2D(Screen.width, Screen.height);
        renderedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        RenderTexture.active = null;

        //Creating the Texture2D that will be applied to the UI RawImage 
        Texture2D newScreenshot = new Texture2D(renderedTexture.width, renderedTexture.height, TextureFormat.RGB24, false);
        newScreenshot.SetPixels(renderedTexture.GetPixels());
        newScreenshot.Apply();

        //Disabling the post process volume after screen has been rendered
        _volume.enabled = false;

        return newScreenshot;
    }

    private bool IsIlluminated(Texture2D renderTexture)
    {
        //Taking the passed render texture and creating a new Texture2D object using its width and height
        var texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBAFloat, false);
        texture2D.ReadPixels(new Rect(0, 0, texture2D.width, texture2D.height), 0, 0, false);
        texture2D.Apply();

        //Creating an array of every pixel in the Texture2D
        var allColors = texture2D.GetPixels();

        //Creating a base float of zero illumination    
        float totalLuminance = 0f;

        //For each pixel on screen, we take its level of red, green, and blue to get an approximate illumination
        foreach (var color in allColors)
        {
            //Multiplying each color to equalize illumination
            totalLuminance += (color.r * 0.2126f) + (color.g * 0.7152f) + (color.b * 0.0722f);
        }

        //Taking the sum of each pixel's illumination and dividing by the total number of pixels to get an average
        var averageLuminance = totalLuminance / allColors.Length;
        Debug.Log("AVERAGE LUMINANCE : " + averageLuminance);

        //No longer need the texture after reading pixels
        Object.Destroy(texture2D);

        //Checking if illumination is greater than arbitrary threshold
        return averageLuminance > _minLuminance;
    }

    //Called when taking a picture of an item verfied as evidence
    private IEnumerator EvidenceDing()
    {
        //Call a screen glitch effect 
        _data.Glitch();

        //wait 0.2 seconds before setting audio sourcde to a random pitch and playing
        yield return new WaitForSeconds(0.2f);
        PlaySound(_evidenceChime);
    }

    private void PlaySound(AudioClip clip)
    {
        //Setting the clip for object audio source
        _audio.clip = clip;

        //Setting a slightly randomized pitch for variation
        _audio.pitch = Random.Range(0.8f, 1.1f);
        _audio.Play();
    }

}
