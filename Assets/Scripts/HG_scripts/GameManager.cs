using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [HideInInspector] public bool _isPaused = false;
    [SerializeField] private CameraController _controller;
    [SerializeField] private TMP_Text _pausedText;
    [SerializeField] private GameObject _pauseButtons;
    [SerializeField] private Animator _pausedAnimator;
    [SerializeField] private AudioSource _pausedAudio;
    [SerializeField] private GameObject _hudBorder;
    [SerializeField] private RawImage _cursorSprite;
    [SerializeField] private GameObject _ghostCam;
     
    [Header("KillMonster variables")]
    [SerializeField] private GameObject _killMonster;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CameraController _cam;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private Transform _faceDirectionPoint;

    private void Start()
    {
        _ghostCam.SetActive(false);
    }
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused )
            {
                if (_controller._hasCamera)
                {
                _pausedAnimator.SetBool("paused", false);
                _pausedText.text = "REC";

                }
                _isPaused = false;
                _controller.enabled = true;
                _pauseButtons.SetActive(false);
                Time.timeScale = 1f;
                _hudBorder.SetActive(true);
                _cursorSprite.enabled = true;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
            else
            {
                _pausedAnimator.SetBool("paused", true);
                _isPaused = true;
                _pausedText.text = "PAUSED";
                _controller.enabled = false;
                _pauseButtons.SetActive(true);
                Time.timeScale = 0f;
                _hudBorder.SetActive(false);
                _cursorSprite.enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }

            if(!_pausedAudio.isPlaying)
            {
                _pausedAudio.Play();
            }
        }

    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void SpawnKillMonster()
    {
        _blackAnim.SetTrigger("blink");
        GameObject _monsterSpawn = Instantiate(_killMonster, _spawnPoint.position, Quaternion.identity);
        MonsterKill _monster = _monsterSpawn.GetComponent<MonsterKill>(); 
        _monster._spawnPoint = _spawnPoint;
        _monster._cam = _cam;
        _monster._blackAnim = _blackAnim;
        _monster._faceDirectionPoint = _faceDirectionPoint;
        StartCoroutine(_monster.KillPlayer());
    }


    public void FreezePlayer(bool _freeze)
    {
        PlayerController.instance._frozen = _freeze;
        _controller._frozen = _freeze;
    }

   
}
