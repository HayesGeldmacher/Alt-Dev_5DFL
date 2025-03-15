using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public static GameInputManager instance { get; private set; }

    public delegate void GameChangeAction(bool isMouse);
    public static event GameChangeAction OnGameDeviceChanged; 

    public enum GameDevice 
    { 
        KeyboardMouse, 
        Gamepad,
    }

    [SerializeField] private GameDevice _activeGameDevice;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        transform.parent = null;

        InputSystem.onActionChange += InputSystem_OnActionChange;
    }

    private void InputSystem_OnActionChange(object arg1, InputActionChange inputActionChange)
    {
        if (inputActionChange == InputActionChange.ActionPerformed && arg1 is InputAction)
        {
            InputAction inputAction = arg1 as InputAction;
            if(inputAction.activeControl.device.displayName == "VirtualMouse")
            {
                //ignore virtual mouse
                return;
            }
            else if(inputAction.activeControl.device is Gamepad)
            {
                if(_activeGameDevice != GameDevice.Gamepad)
                {
                    ChangeActiveGameDevice(GameDevice.Gamepad);
                }
            }
            else
            {
                if(_activeGameDevice != GameDevice.KeyboardMouse)
                {
                    ChangeActiveGameDevice(GameDevice.KeyboardMouse);
                }
            }
        }
    }

    public void ChangeActiveGameDevice(GameDevice activeGameDevice)
    {



        this._activeGameDevice = activeGameDevice;
        Debug.Log("New Active Game Device: " + activeGameDevice);

        bool isMouse = true;

        if(_activeGameDevice == GameDevice.Gamepad)
        {
            isMouse = false;
        }
       

        //This is where we want shit to happen!
        OnGameDeviceChanged?.Invoke(isMouse);
    }

    
}
