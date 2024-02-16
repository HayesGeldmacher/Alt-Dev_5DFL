using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : Interactable
{
    public List<Texture> _textures = new List<Texture>();
    [SerializeField] private Material _material;
    private int _currentTexture;
    
    // Start is called before the first frame update
    private void Start()
    {
        base.Start();
        _currentTexture = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        _currentTexture += 1;
        if(_currentTexture > _textures.Count - 1)
        {
            _currentTexture = 0;
        }
        _material.mainTexture = _textures[_currentTexture];
        //_material.emissiveMap = _textures[_currentTexture];
        _material.SetTexture("_EmissionMap", _textures[_currentTexture]);

    }
}
