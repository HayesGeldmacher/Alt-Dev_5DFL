using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]


public class DataMosh : MonoBehaviour
{

    public Material DMat; //datamosh material
    
    void Start()
    {
        this.GetComponent<Camera>().depthTextureMode = DepthTextureMode.MotionVectors;
        //generate the motion vector texture 
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Debug.Log("FUCK");
        Graphics.Blit(src, dest, DMat);
    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalInt("_Button", Input.GetButton("Fire1") ? 1 : 0);
    }
}
