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
        float mouseX = Input.GetAxis("ControllerX") * _mouseSensitivityX;
        float mouseY = Input.GetAxis("ControllerY") * _mouseSensitivityY;

      
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -20, 50);

        _yRotation -= mouseX;
        _yRotation = Mathf.Clamp(_yRotation, _minRotationY, _maxRotationY);

        Transform parent = transform.parent;

        //parent.Rotate(Vector3.up * mouseX);
        
        //float clampedRot = Mathf.Clamp(parent.localRotation.y, _minRotationY, _maxRotationY);
       // parent.localRotation = Quaternion.Euler(parent.localRotation.x, parent.localRotation.y, parent.localRotation.z);
        
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        parent.localRotation = Quaternion.Euler(0, -_yRotation, 0);
    }
}
