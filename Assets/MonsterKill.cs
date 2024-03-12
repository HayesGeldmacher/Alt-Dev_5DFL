using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKill : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camLookPoint;
    [SerializeField] private CameraController _cam;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private Transform _faceDirectionPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        KillPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillPlayer()
    {
        transform.position = _spawnPoint.position;
        Vector3 relativePos = _faceDirectionPoint.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

        PlayerController.instance._frozen = true;
        _cam.SetCameraLook(_camLookPoint);

        _blackAnim.SetTrigger("black");
    }

    public void Blackout()
    {
        _blackAnim.SetTrigger("blackInstant");
    }
}
