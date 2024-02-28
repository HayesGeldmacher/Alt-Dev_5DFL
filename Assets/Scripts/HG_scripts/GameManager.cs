using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    //This singleton creates a locatable script instance that can be located easily from any other script!
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Manager is present! NOT GOOD!");
            return;
        }

        instance = this;
    }

    #endregion

    private bool _isPaused = false;
    [SerializeField] private CameraController _controller;
    [SerializeField] private TMP_Text _pausedText;
    [SerializeField] private Animator _pausedAnimator;
    [SerializeField] private AudioSource _pausedAudio;

   
  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused )
            {
                if (_controller._hasCamera)
                {
                _pausedAnimator.SetBool("paused", false);
                _pausedText.text = "RECORDING";

                }
                _isPaused = false;
                _controller.enabled = true;
                Time.timeScale = 1f;
                


            }
            else
            {
                _pausedAnimator.SetBool("paused", true);
                _isPaused = true;
                _pausedText.text = "PAUSED";
                _controller.enabled = false;
                Time.timeScale = 0f;

            }

            if(!_pausedAudio.isPlaying)
            {
                _pausedAudio.Play();
            }
        }

    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
