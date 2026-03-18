using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIndexUpdate : MonoBehaviour
{

    public int loadIndex = 0;
    public bool forceReset = false;
      void Awake()
      {

        if(forceReset)
        {
            //ex. when we finish credits, set to begin game at start
            PlayerPrefs.SetInt("loadIndex", loadIndex);
        }
        else
        {
            if (PlayerPrefs.HasKey("loadIndex"))
            {
                int currentLoad = PlayerPrefs.GetInt("loadIndex");
                if (loadIndex > currentLoad)
                {
                    PlayerPrefs.SetInt("loadIndex", loadIndex);
                    Debug.Log("set load index to: " + PlayerPrefs.GetInt("loadIndex"));
                }
            }
            else
            {
                PlayerPrefs.SetInt("loadIndex", loadIndex);

            }

        }


        Debug.Log("set load index to: " + PlayerPrefs.GetInt("loadIndex"));
      }
}
