using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageTurnOn : MonoBehaviour
{

    [SerializeField] private Animator _garageAnim;

    // Start is called before the first frame update
    void Start()
    {
        _garageAnim = transform.GetComponent<Animator>();
        _garageAnim.SetTrigger("appear");
    }


}
