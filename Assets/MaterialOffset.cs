using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffset : MonoBehaviour
{
    public Material[] _materials;
    public float _offSetSpeed;
    public bool _tiling = true;
    private float _newOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_tiling)
        {
            _newOffset += (Time.deltaTime * _offSetSpeed);
            foreach(Material material in _materials)
            {
                material.SetTextureOffset("_MainTex", new Vector2(0, _newOffset));
            }
        }
    }
}
