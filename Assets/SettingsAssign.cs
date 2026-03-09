using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class SettingsAssign : MonoBehaviour
{

    public bool assignCameraSettings = false;
    public CameraController camController;

    public PostProcessProfile worldBrightnessProfile;
    public AutoExposure exposure;

    public PostProcessProfile menuBrightnessProfile;
    public AutoExposure menuExposure;

    //true for days 1, 2, 3 
    public bool hasSecondProfiles = false;

    public PostProcessProfile worldBrightnessProfile2;
    public AutoExposure exposure2;

    public PostProcessProfile menuBrightnessProfile2;
    public AutoExposure menuExposure2;


    void Awake()
    {
        if (worldBrightnessProfile != null)
        {
            worldBrightnessProfile.TryGetSettings(out exposure);
        }
        if (menuBrightnessProfile != null)
        {
            menuBrightnessProfile.TryGetSettings(out menuExposure);

        }

        if (hasSecondProfiles)
        {
            if (worldBrightnessProfile2 != null)
            {

                worldBrightnessProfile2.TryGetSettings(out exposure2);

            }
            if (menuBrightnessProfile2 != null)
            {
                menuBrightnessProfile2.TryGetSettings(out menuExposure2);
            }
        }

        AssignBrightnessFromPrefs();
        SetCameraPrefs();
    }


    public void AssignPlayerPreferences()
    {
        AssignBrightnessFromPrefs();
        if (assignCameraSettings)
        {
            SetCameraPrefs();
        }
    }
    public void AssignBrightnessFromPrefs()
    {
        if (PlayerPrefs.HasKey("worldBrightness"))
        {

            float value = PlayerPrefs.GetFloat("worldBrightness");
            if (exposure != null)
            {

                exposure.keyValue.value = value;
            }
            if (hasSecondProfiles && exposure2 != null)
            {
                exposure2.keyValue.value = value;
            }
            Debug.Log("Set world brightness in player prefs!");

        }
        else
        {
            Debug.Log("No player pref exists for world brightness!");
            if (exposure != null)
            {
                exposure.keyValue.value = 1.0f;

            }
            if (hasSecondProfiles && exposure2 != null)
            {
                exposure2.keyValue.value = 1.0f;
            }

        }

        if (PlayerPrefs.HasKey("menuBrightness"))
        {
            float value = PlayerPrefs.GetFloat("menuBrightness");
            if (menuExposure != null)
            {
                menuExposure.keyValue.value = value;
            }
            if (hasSecondProfiles && menuExposure2 != null)
            {
                menuExposure2.keyValue.value = value;
            }
            Debug.Log("Set menu brightness in player prefs!");
        }
        else
        {
            Debug.Log("No player pref exists for menu brightness!");
            if (menuExposure != null)
            {
                menuExposure.keyValue.value = 1.0f;
            }
            if (hasSecondProfiles && menuExposure2 != null)
            {
                menuExposure2.keyValue.value = 1.0f;
            }
        }
    }

    public void SetCameraPrefs()
    {
       camController.SetPlayerPrefs();
    }

}
