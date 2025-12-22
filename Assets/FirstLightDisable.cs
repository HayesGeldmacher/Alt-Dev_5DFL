using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLightDisable : MonoBehaviour
{



    [SerializeField] public Animator lightAnim;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            lightAnim.SetTrigger("fade");
            Destroy(gameObject);
        }
    }
}
