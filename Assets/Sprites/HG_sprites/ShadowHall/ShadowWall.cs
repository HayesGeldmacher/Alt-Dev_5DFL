using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWall : MonoBehaviour
{
    //3D Version
    public Texture2D[] textures;
    public MeshRenderer rend;
    public float speed;
    public float time;

    public bool emissive = false;

    void Update()
    {
        time += Time.deltaTime * speed;
        int texID = Mathf.RoundToInt(time) % textures.Length;
        rend.material.SetTexture("_MainTex", textures[texID]);
        if (!emissive) return;
        rend.material.SetTexture("_EmissionMap", textures[texID]);
    }


}
