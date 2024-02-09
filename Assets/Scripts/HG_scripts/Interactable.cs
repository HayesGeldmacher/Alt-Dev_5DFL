using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
   //This script is a parent script that many objects will inherit from
   //writing "virtual" in front of a function means that children scripts can add to/edit the function
    public virtual void Interact()
    {
        Debug.Log("Interacted!");
    }
}
