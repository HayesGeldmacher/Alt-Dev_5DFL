using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class TextGameManager : MonoBehaviour
{



    private bool _ended = false;
    private bool _endedInput = false;

    [Header("AnimationVariables")]
    [SerializeField] private Animator _canvasAnim;
    [SerializeField] private Animator _backGroundAnim;
    [SerializeField] private CameraController _camControl;
    [SerializeField] private GameObject _brokenMonitor;
    [SerializeField] private GameObject _workingMonitor;


    [Header("TextVariables")]
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private TMP_Text _optionText1;
    [SerializeField] private TMP_Text _optionText2;
    [SerializeField] private TMP_Text _optionText3;

    [SerializeField] private int _currentOptions;
    [SerializeField] private int _currentPassage;

    [SerializeField] private TextEncounter _currentEncounter;
    public TextEncounter[] _encounterList;

    [Header("Audio Variables")]
    [SerializeField] private AudioSource _interactSound;
    [SerializeField] private AudioSource _staticAudio;
    [SerializeField] private AudioSource _quitAudio;
    [SerializeField] private AudioSource _encounterSound;

    [Header("Black Boxes")]
    [SerializeField] private GameObject _blackBox1;
    [SerializeField] private GameObject _blackBox2;
    [SerializeField] private GameObject _blackBox3;
    [SerializeField] private GameObject _promptBox;

    [Header("Buttons")]
    [SerializeField] private GameObject _buttonMaster;
    [SerializeField] private GameObject _button1;
    [SerializeField] private GameObject _button2;
    [SerializeField] private GameObject _button3;

    private bool _paused = false;
    [SerializeField] private GameObject _pauseBox;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _monster;

    [SerializeField] private AudioSource _breatheAudio;

    [SerializeField] private GameObject _mouseCursor;
    [SerializeField] private GameObject _mouseCursorInteract;
    [SerializeField] private GameObject _pauseMaster;

    private bool _canInteract = true;
    //if you must select all options, set _allOptionsList to 3

    //if one option is correct


    // Start is called before the first frame update
    void Start()
    {
        _currentPassage = 0;
        _pauseBox.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(_ended || _paused || _endedInput) return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && _currentOptions > 0)
        {
            ChooseOption(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _currentOptions > 1)
        {
            ChooseOption(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && _currentOptions > 2)
        {
            ChooseOption(2);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _currentOptions <= 0)
        {
            ChooseOption(0);
        }

    }

    private IEnumerator StartInteraction()
    {
        yield return new WaitForSeconds(1);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _pauseMaster.SetActive(false);
    }

    public void StartGame()
    {

        SetEncounter(0);
        StartCoroutine(StartInteraction());
    }

    public void ChooseOption(int _option)
    {
        if (_ended) return;
       
        if (_currentOptions > 0)
        {
            TextEncounter _oldEncounter = _currentEncounter;
            SetEncounter(_currentEncounter._answerPaths[_option]);

            if (_oldEncounter._keepOption)
            {
                bool _shouldKeepOption = _oldEncounter._shouldKeepOption[_option];

                if (!_shouldKeepOption)
                {
                    _oldEncounter._options.RemoveAt(_option);
                    _oldEncounter._answerPaths.RemoveAt(_option);

                }

            }
            else
            {
                _oldEncounter._options.RemoveAt(_option);
                _oldEncounter._answerPaths.RemoveAt(_option);
            }
        }
        else
        {
            SetEncounter(_currentEncounter._finalPath);
        }

        PlaySound();

    }

    private void SetEncounter(int _nextEncounterNum)
    {


        //Set option to -1 in order to fully quit the game!
        if (_nextEncounterNum == -1) { QuitGame(); return; }


        TextEncounter _newEncounter = _encounterList[_nextEncounterNum];

        if (_newEncounter._interactive && _newEncounter._options.Count <= 0)
        {
            _newEncounter = _encounterList[_newEncounter._finalPath];
        }
        _currentEncounter = _newEncounter;

        Debug.Log("CURRENTOPTIONS: " + _currentEncounter._options.Count);
        _currentOptions = _currentEncounter._options.Count;

        _promptText.text = _currentEncounter._prompt;


        if (_currentOptions > 0)
        {
            _optionText1.text = "1: " + _currentEncounter._options[0];
            _blackBox1.SetActive(true);
            _button1.SetActive(true);

            if (_currentOptions > 1)
            {
                _optionText2.text = "2: " + _currentEncounter._options[1];
                _blackBox2.SetActive(true);
                _button2.SetActive(true);

                if (_currentOptions > 2)
                {
                    _optionText3.text = "3: " + _currentEncounter._options[2];
                    _blackBox3.SetActive(true);
                    _button3.SetActive(true);
                }
                else
                {
                    _optionText3.text = "";
                    _blackBox3.SetActive(false);
                    _button3.SetActive(false);
                }
            }
            else
            {
                _optionText2.text = "";
                _optionText3.text = "";

                _blackBox2.SetActive(false);
                _blackBox3.SetActive(false);
                
                _button2.SetActive(false);
                _button3.SetActive(false);
            }
        }
        else
        {

            if (_currentEncounter._hideOptions)
            {
                _optionText1.text = "";
                _blackBox1.SetActive(false);
                _promptBox.SetActive(false);
                _button1.SetActive(false);
            }
            else
            {
                _optionText1.text = "Continue";
                _blackBox1.SetActive(true);
                _promptBox.SetActive(true);
                _button1.SetActive(true);

            }
            _optionText2.text = "";
            _optionText3.text = "";

            _blackBox2.SetActive(false);
            _blackBox3.SetActive(false);

            _button2.SetActive(false);
            _button3.SetActive(false);
        }



        //Set the encounter background animation image, if one is determined
        int _animSet = _currentEncounter._imageAnimSet;

        if(_animSet > -1)
        {
            _backGroundAnim.SetInteger("textProgress", _animSet);
        }

        if (_currentEncounter._hasSound)
        {
            PlayEncounterSound(_currentEncounter._soundClip);
        }

    }

    private void PlaySound()
    {
        _interactSound.pitch = Random.Range(0.8f, 1.1f);
        _interactSound.Play();
    }

    private void PlayEncounterSound(AudioClip _clip)
    {
        _encounterSound.clip = _clip;
        _encounterSound.Play();
    }

    private void QuitGame()
    {
        if (!_ended)
        {
            _ended = true;
            _mouseCursor.SetActive(false);
            StartCoroutine(DisableGame());
        }
    }

    private IEnumerator DisableGame()
    {
        Debug.Log("I FUCKIGN QUIT!");
        _canvasAnim.SetBool("visible", false);
        _monster.SetActive(true);
        _brokenMonitor.SetActive(true);
        _workingMonitor.SetActive(false);
        _quitAudio.Play();
        _button1.SetActive(false);
        _button2.SetActive(false);
        _button3.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        PlayerController.instance._frozen = false;
        _camControl._frozen = false;
        _staticAudio.Stop();
        _breatheAudio.Play();
        this.enabled = false;
        GameManager.instance._inTextGame = false;
        _mouseCursorInteract.SetActive(true);
        _pauseMaster.SetActive(true);
    }

    public void EndScare()
    {
        QuitGame();
    }

    public void Pause()
    {
            _paused = true;
            _dialogueBox.SetActive(false);
            _pauseBox.SetActive(true);
            _canInteract = false;
        _mouseCursor.transform.GetChild(0).gameObject.SetActive(false);
        _buttonMaster.SetActive(false);
    }

    public void UnPause()
    {
        _paused = false;
        _dialogueBox.SetActive(true);
        _pauseBox.SetActive(false);
        _canInteract = true;
        _mouseCursor.transform.GetChild(0).gameObject.SetActive(true);
        _buttonMaster.SetActive(true);
    }

    public void SetEndInput()
    {
        //disables input once the final scary animation plays - no pausing, no repeating previous scenario!
        _endedInput = true;
    }


    public void OptionSeleted0()
    {
        ChooseOption(0);
    }

    public void OptionSelected1()
    {
        ChooseOption(1);
    }

    public void OptionSelected2()
    {
       ChooseOption(2);
    }

}
