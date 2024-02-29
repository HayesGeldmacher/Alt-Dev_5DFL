using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{

    [SerializeField] private float _neededPauseTime;
    private float currentPauseTime = 0;

    // Update is called once per frame
    void Update()
    {

        currentPauseTime += Time.deltaTime;
        
        if (Input.GetMouseButtonDown(0))
        {
           if(currentPauseTime > _neededPauseTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
        }
    }

}
