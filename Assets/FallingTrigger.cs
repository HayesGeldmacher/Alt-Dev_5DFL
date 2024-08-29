using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{

    private BoxCollider _collider;
    [SerializeField] private Animator _camAnim;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _camAnim.SetTrigger("fall");
            _collider.enabled = false;
            Debug.Log("Collided with!");
        }
    }
}
