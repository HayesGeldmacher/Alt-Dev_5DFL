using UnityEngine;

[ExecuteInEditMode]
public class CRT : MonoBehaviour
{
    public Material material;
    [SerializeField] private float _hardness;
    [SerializeField] private float _pixHardness;
    [SerializeField] private Vector4 _displayWarp =  new Vector4(0.03125f, 0.04166f, 0f, 0f);  
    [SerializeField] private float _resolution = 4;
    [SerializeField] private float _maskDark;
    [SerializeField] private float _maskLight;
    

    [Header("Breathing Anim")]
    public bool _breathing = false;
    public float _breathUpSpeed;
    public float _breathDownSpeed;
    public bool _goingUp;
    public float _maxRes;
    public float _minRes;
    public float _currentWaitTime;
    public float _waitTime;
    public bool _waiting = false;

    [Header("Stop Fields")]
    public bool _shouldStop = false;
    public float _endRes = 2.8f;

    //default values range!!

    // Hardness of scanline.
    //  -8.0 = soft
    // -16.0 = medium

    // Hardness of pixels in scanline.
    // -2.0 = soft
    // -4.0 = hard

    // Display warp.
    // 0.0 = none
    // 1.0/8.0 = extreme

    // resolution scale

    // Use this for initialization
    void Start()
    {
        material = new Material(Shader.Find("Hidden/CRT"));
    }


    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetInt("hardScan", (int)_hardness);
        material.SetInt("hardPix", (int)_pixHardness);
        material.SetInt("resScale", (int)_resolution);
        material.SetVector("warp", _displayWarp);
        material.SetFloat("maskDark", (float)_maskDark);
        material.SetFloat("maskLight", (float)_maskLight);
        material.SetTexture("_MainTex", source);
        Graphics.Blit(source, destination, material);
    }

    private void Update()
    {
        if (_breathing)
        {

            float distance = _resolution - _endRes;
            
            if(_shouldStop && (Mathf.Abs(distance) < 0.5f))
            {
                _resolution = _endRes;
                _breathing = false;
            }

            if (_waiting)
            {
                
                _currentWaitTime -= Time.deltaTime;
                if(_currentWaitTime <= 0)
                {
                    _waiting = false;
                }
            }
            else if (_goingUp)
            {
                _resolution += (Time.deltaTime * _breathUpSpeed);
                if(_resolution > _maxRes)
                {
                    _waiting = true;
                    _currentWaitTime = _waitTime;
                    _goingUp = false;
                }

            }
            else
            {
                _resolution -= (Time.deltaTime * _breathDownSpeed);
                if(_resolution < _minRes)
                {
                    _waiting = false;
                    _currentWaitTime = _waitTime;
                    _goingUp = true;
                }
            }
        }
    }

    public void EndBreathing()
    {
        _shouldStop = true;
    }

    public void StartBreathing()
    {
        _shouldStop = false;
        _breathing = true;
    }
}
