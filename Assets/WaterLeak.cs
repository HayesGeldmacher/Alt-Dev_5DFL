using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class WaterLeak : MonoBehaviour
{
    [SerializeField] private AudioSource _waterAudio;
    [SerializeField] private AudioClip[] _waterClips;
    [SerializeField] private float _maxWait;
    [SerializeField] private float _minWait;
    [SerializeField] private float _currentWait;
    [SerializeField] private Animator _waterDropAnim;
    private void Start()
    {
        _currentWait = _maxWait;
    }

    private void Update()
    {
        _currentWait -= Time.deltaTime;

        if(_currentWait < 0)
        {
            _currentWait = Random.Range(_minWait, _maxWait);
            PlayWaterAnimation();
        }
    }

    private void PlayWaterAnimation()
    {
        _waterDropAnim.SetTrigger("drop");
    }

    public void PlayWaterSound()
    {
        _waterAudio.clip = _waterClips[Random.Range(0, _waterClips.Length)];
        _waterAudio.pitch = (Random.Range(0.8f, 1.2f));
        _waterAudio.Play();
    }
}
