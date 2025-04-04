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
        material.SetInt("resScale", (int)_resolution);
        material.SetVector("warp", _displayWarp);
        material.SetFloat("maskDark", (float)_maskDark);
        material.SetFloat("maskLight", (float)_maskLight);
        material.SetTexture("_MainTex", source);
        Graphics.Blit(source, destination, material);
    }
}
