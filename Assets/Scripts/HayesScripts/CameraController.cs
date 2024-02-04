using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //SerializeField makes the variable visible in the inspector even when its private!
    [Header ("Look Variables")]
    [SerializeField] private float _mouseSensitivityX;
    [SerializeField] private float _mouseSensitivityY;
    private float _xRotation = 0f;
    private float _yRotation = 0f;
    private float _zRotation = 0f;

    //Here, we are getting a reference to the actual player that the camera is a child of
    [SerializeField] private Transform _playerBody;

    //This decides what elements we can interact with in the game world
    [SerializeField] private LayerMask _interactable;


    void Start()
    {
        //This makes the cursor invisible and locked to screen when playing the game
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Here, we are getting the actualy mouse movement from the player and converting it to variables
        //All inputs should be multiplied Time.deltaTime in order for physics to work correctly
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivityY * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        
        //The below snippet lerps the camera from side to side depending on which direction the player is walking
        float xMove = Input.GetAxis("Horizontal");
        if (xMove > 0.1f)
        {
            _zRotation = Mathf.Lerp(_zRotation, -1, 6 * Time.deltaTime);

        }
        else if (xMove < -0.1f)
        {
            _zRotation = Mathf.Lerp(_zRotation, 1, 6 * Time.deltaTime);
        }
        else
        {
            _zRotation = Mathf.Lerp(_zRotation, 0, 6 * Time.deltaTime);
        }

        
        //This rotates the player's body side to side when aiming with the camera
        _playerBody.Rotate(Vector3.up * mouseX);

        //This rotates the camera up and down, forward/backward, and side to side
        transform.localRotation = Quaternion.Euler(_xRotation, 0, _zRotation);
    }

   
}
