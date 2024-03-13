using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKill : MonoBehaviour
{
    [HideInInspector] public Animator _anim;
    [HideInInspector] public Transform _spawnPoint;
    [SerializeField] private Transform _camLookPoint;
    [HideInInspector] public CameraController _cam;
    [HideInInspector] public Animator _blackAnim;
    [HideInInspector] public Transform _faceDirectionPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator KillPlayer()
    {
        transform.position = _spawnPoint.position;
        Vector3 relativePos = _faceDirectionPoint.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

        PlayerController.instance._frozen = true;
        PlayerController.instance.StopCrouchInstant();
        _cam.SetCameraLook(_camLookPoint);

        yield return new WaitForSeconds(2);
        GameManager.instance.ReloadLevel();
    }

    public void Blackout()
    {
        _blackAnim.SetTrigger("blackInstant");
    }
}
