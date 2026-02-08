using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeTVFinale : MonoBehaviour
{
    //3D Version
    public Texture2D[] textures;
    public MeshRenderer rend;



    public void SetTexture(int texID)
    {
        rend.material.SetTexture("_MainTex", textures[texID]);
        rend.material.SetTexture("_EmissionMap", textures[texID]);
    }


   
}
