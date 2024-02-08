using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    //This singleton creates a locatable script instance that can be located easily from any other script!
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory present!! NOT GOOD!");
            return;
        }

        instance = this;

        _hudCanvas.SetActive(true);
    }

    #endregion

}
