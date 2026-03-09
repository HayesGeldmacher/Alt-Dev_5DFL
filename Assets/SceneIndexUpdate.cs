using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIndexUpdate : MonoBehaviour
{

    public int loadIndex = 0;
    
      void Awake()
      {
        PlayerPrefs.SetInt("loadIndex", loadIndex);
      }
}
