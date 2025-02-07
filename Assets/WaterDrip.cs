using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrip : MonoBehaviour
{


    [SerializeField] private AudioClip[] _waterClips;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _maxWait;
    [SerializeField] private float _minWait;
    [SerializeField] private float _currentWait;

    [SerializeField] private Animator _rainDrop;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _currentWait = _maxWait;
    }

    // Update is called once per frame
    void Update()
    {
        _currentWait -= Time.deltaTime;
        
        if(_currentWait <= 0)
        {
            PlayRainDrop();
            _currentWait = Random.Range(_minWait, _maxWait);
        }
    }

    public void PlayWaterDrop()
    {
        Debug.Log("PLAYED RAIN DROP!");
        _audio.clip = _waterClips[Random.Range(0, _waterClips.Length)];
        _audio.pitch = Random.Range( 0.8f, 1.2f);

        _audio.Play();
    }

    private void PlayRainDrop()
    {
        _rainDrop.SetTrigger("drop");
    }
    

}
