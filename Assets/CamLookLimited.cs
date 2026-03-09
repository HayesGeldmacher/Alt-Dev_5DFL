using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookLimited : MonoBehaviour
{
    public float _mouseSensitivityX;
    public float _mouseSensitivityY;
    private float _xRotation = 0;
    private float _yRotation = 0;

    public float _minRotationY;
    public float _maxRotationY;


    [Header("Interaction Fields")]
    public bool interactCapable = false;
    private bool _frozen = false;

    public GameManager _manager;

    public bool _inverted = true;

    [Header("Settings Fields")]
    [SerializeField] private float prefCamSpeedX = 1.0f;
    [SerializeField] private float prefCamSpeedY = 1.0f;
    public float prefCamInvertY = 1.0f;

    private void Awake()
    {
        GameManager.pauseInstance += FreezeCam;
        GameManager.unPauseInstance += UnFreezeCam;

        SetPlayerPrefs();
    }
    
    // Start is called before the first frame update
    void Start()
    {

        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Here, we are getting the actualy mouse movement from the player and converting it to variables
        //All inputs should be multiplied Time.deltaTime in order for physics to work correctly

        if (_frozen) return;
    
        float mouseX = Input.GetAxis("ControllerX") * _mouseSensitivityX * prefCamSpeedX;
        float mouseY = Input.GetAxis("ControllerY") * _mouseSensitivityY * prefCamSpeedY;

      
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -20, 50);

        float zRot;
        if (_inverted)
        {
            zRot = 180f;
        }
        else
        {
            zRot = 0f;
        }


        _yRotation -= mouseX;
        _yRotation = Mathf.Clamp(_yRotation, _minRotationY, _maxRotationY);

        Transform parent = transform.parent;

        //parent.Rotate(Vector3.up * mouseX);

        //float clampedRot = Mathf.Clamp(parent.localRotation.y, _minRotationY, _maxRotationY);
        // parent.localRotation = Quaternion.Euler(parent.localRotation.x, parent.localRotation.y, parent.localRotation.z);

      

        transform.localRotation = Quaternion.Euler(_xRotation, 0, zRot);

        if (_inverted)
        {
            parent.localRotation = Quaternion.Euler(0, _yRotation, 0);
        }
        else
        {
          parent.localRotation = Quaternion.Euler(0, -_yRotation, 0);
        }
    }


    public void FreezeCam()
    {
        _frozen = true;
    }

    public void UnFreezeCam()
    {
        _frozen = false;
    }

    public void SetPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("cameraSpeedX"))
        {
            float value = PlayerPrefs.GetFloat("cameraSpeedX");
            prefCamSpeedX = value;
            Debug.Log("Set cam speed x volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for camera speed X!");
            prefCamSpeedX = 1.0f;

        }
        if (PlayerPrefs.HasKey("cameraSpeedY"))
        {
            float value = PlayerPrefs.GetFloat("cameraSpeedY");
            prefCamSpeedY = value;
            Debug.Log("Set cam speed Y volume in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for camera speed Y!");
            prefCamSpeedY = 1.0f;

        }

        prefCamInvertY = 1.0f;
        if (PlayerPrefs.HasKey("camInvert"))
        {
            int value = PlayerPrefs.GetInt("camInvert");
            float newValue = (float)value;
            if (newValue == -1.0f)
            {
                prefCamInvertY = newValue;
            }
        }
        Debug.Log("Set camera value to : " + prefCamInvertY);
    }
}
