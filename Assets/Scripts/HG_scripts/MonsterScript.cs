using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public List<Texture> _textures = new List<Texture>();
    [SerializeField] private Material _material;
    private int _currentTexture;
    private float _currentTime;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > 1)
        {
            _currentTime = 0;
            Swap();
        }
    }

    private void Swap()
    {
        _currentTexture += 1;
        if (_currentTexture > _textures.Count - 1)
        {
            _currentTexture = 0;
        }
        _material.mainTexture = _textures[_currentTexture];
        //_material.emissiveMap = _textures[_currentTexture];
        _material.SetTexture("_EmissionMap", _textures[_currentTexture]);

    }
}
