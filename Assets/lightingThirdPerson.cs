using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightingThirdPerson : MonoBehaviour
{


    public float _lightValue;
    public float _lerpSpeed;
    public bool _lerping = false;
    public float _currentLerpValue;



    private void Update()
    {
        if (_lerping)
        {
            float newValue = Mathf.Lerp(RenderSettings.ambientIntensity, _lightValue, _lerpSpeed * Time.deltaTime);
            RenderSettings.ambientIntensity = newValue;

            float diff = RenderSettings.ambientIntensity - newValue;
            if (diff <= 0.05)
            {
                //_lerping = false;
            }

            _currentLerpValue = RenderSettings.ambientIntensity;
        }
    }

}
