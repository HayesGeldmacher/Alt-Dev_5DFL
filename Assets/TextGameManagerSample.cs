using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class TextGameManagerSample : MonoBehaviour
{
    [Header("Encounter Variables")]
    /// List of all available text encounter scenarios
    /// Each TextEncounter includes variables determining prompt, dialogue options, and background image
    public TextEncounter[] _encounterList;
    //Keeps track of the currently displayed text encounter
    [SerializeField] private TextEncounter _currentEncounter;

    [Header("TextVariables")]
    //Text slots for encounter prompt and dialogue choices
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private TMP_Text[] _optionTexts;

    //Determines how many current dialogue options are available - start with 3 options as default
    private int _currentOptions = 3;

    //On-screen UI buttons determine which dialogue option is chosen during each encounter
    [Header("Buttons")]
    [SerializeField] private GameObject[] _buttons;

    [Header("Black Boxes")]
    //Prompt Box appears behind the prompt dialogue 
    [SerializeField] private GameObject _promptBox;
    //Black boxes appear behind currently available dialogue options
    [SerializeField] private GameObject[] _blackBoxes;

    [Header("AnimationVariables")]
    //Animates the render texture that the current text encounter is displayed on
    [SerializeField] private Animator _canvasAnim;
    //Determines the background image that accompanies each text encounter
    [SerializeField] private Animator _backGroundAnim;

    [Header("Audio Variables")]
    //Played every time player chooses a dialogue option
    [SerializeField] private AudioSource _interactSound;
    //Played if current text encounter has an associated audio clip
    [SerializeField] private AudioSource _encounterSound;

    //Determines if ExitGame function has begun
    private bool _ended = false;

    //Pause function called from PauseManager script
    private bool _paused = false;

    //StartGame function is called by "StartGame" on-screen UI button
    public void StartGame()
    {
        //Setting cursor visbility to false and locking within the bounds of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        //Initiating the first TextGame encounter
        SetEncounter(0);
    }

    //OptionSelected functions are called by "DialogueOption" on-screen UI buttons
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

    //Called by above OptionSelected functions
    public void ChooseOption(int _option)
    {
        //Only process input if game is unpaused and currently being played
        if (_ended || _paused) return;

        //Checks if there are still available dialogue options in current text encounter
        if (_currentOptions > 0)
        {
            //Keeping previous text encounter saved as local variable before moving to next encounter
            TextEncounter oldEncounter = _currentEncounter;

            //Moving to the next text encounter determined by chosen dialogue option
            SetEncounter(_currentEncounter._answerPaths[_option]);

            //Checking if the previous text encounter should have chosen dialogue paths kept
            if (oldEncounter._keepOption)
            {
                //Checking if particular chosen option should be kept
                bool _shouldKeepOption = oldEncounter._shouldKeepOption[_option];

                //If chosen option doesn't keep, remove both chosen option and associated answer path
                if (!_shouldKeepOption)
                {
                    oldEncounter._options.RemoveAt(_option);
                    oldEncounter._answerPaths.RemoveAt(_option);
                }
            }
            //If previous encounter doesn't keep options, remove both chosen option and associated answer path
            else
            {
                oldEncounter._options.RemoveAt(_option);
                oldEncounter._answerPaths.RemoveAt(_option);
            }
        }
        //If no available dialogue options, move to next text encounter
        else
        {
            SetEncounter(_currentEncounter._finalPath);
        }

        PlayInteractSound();
    }

    private void SetEncounter(int _nextEncounterNum)
    {
        //Setting text encounter final path to -1 quits the game
        if (_nextEncounterNum == -1) { DisableGame(); return;}

        //Creating local variable for the next chosen text encounter
        TextEncounter _newEncounter = _encounterList[_nextEncounterNum];

        //If new text encounter has no dialogue options, skip to following text encounter
        if (_newEncounter._interactive && _newEncounter._options.Count <= 0)
        {
            _newEncounter = _encounterList[_newEncounter._finalPath];
        }

        _currentEncounter = _newEncounter;

        //Deteterming how many dialogue options in new text encounter
        Debug.Log("Current Options: " + _currentEncounter._options.Count);
        _currentOptions = _currentEncounter._options.Count;

        //Setting the text encounter prompt text
        _promptText.text = _currentEncounter._prompt;

        //Set text, black boxes, and buttons according to available dialogue options
        DetermineOptions();
       
        //Set the encounter background animation image, if one is determined
        int _animSet = _currentEncounter._imageAnimSet;

        //if animSet = -1, do not update background image
        if(_animSet > -1)
        {
            _backGroundAnim.SetInteger("textProgress", _animSet);
        }
      
        //Checking if current text encounter has associated audio clip to play
        if (_currentEncounter._hasSound)
        {
            PlayEncounterSound(_currentEncounter._soundClip);
        }
    }

    private void DetermineOptions()
    {
        //Setting each available dialogue option to associated text encounter dialogue
        if (_currentOptions > 0)
        {
            _optionTexts[0].text = "1: " + _currentEncounter._options[0];
            _blackBoxes[0].SetActive(true);
            _buttons[0].SetActive(true);

            if (_currentOptions > 1)
            {
                _optionTexts[1].text = "2: " + _currentEncounter._options[1];
                _blackBoxes[1].SetActive(true);
                _buttons[1].SetActive(true);

                if (_currentOptions > 2)
                {
                    _optionTexts[2].text = "3: " + _currentEncounter._options[2];
                    _blackBoxes[2].SetActive(true);
                    _buttons[2].SetActive(true);
                }
                else
                {
                    _optionTexts[2].text = "";
                    _blackBoxes[2].SetActive(false);
                    _buttons[2].SetActive(false);
                }
            }
            else
            {
                //If only one available dialogue option, disable second and third options
                DisableSecondaryOptions();
            }
        }
        //If zero dialogue options, disable first option
        else
        {
            //Checking if current text encounter hides all dialogue
            if (_currentEncounter._hideOptions)
            {
                _optionTexts[0].text = "";
                _blackBoxes[0].SetActive(false);
                _promptBox.SetActive(false);
                _buttons[0].SetActive(false);
            }
            else
            {
                _optionTexts[0].text = "Continue";
                _blackBoxes[0].SetActive(true);
                _promptBox.SetActive(true);
                _buttons[0].SetActive(false);

            }

            //After setting firt dialogue option, disable second and third option
            DisableSecondaryOptions();
        }
    }

    //Disables the text, button, and box for second and third dialogue options
    private void DisableSecondaryOptions()
    {
        _optionTexts[1].text = "";
        _optionTexts[2].text = "";

        _blackBoxes[1].SetActive(false);
        _blackBoxes[2].SetActive(false);

        _buttons[1].SetActive(false);
        _buttons[2].SetActive(false);
    }

    //Function called from "Quit" external UI button
    private void DisableGame()
    {
        if (!_ended)
        {
            _ended = true;
            Debug.Log("Quitting Game");

            //Disabling button inputs before disappear animation
            foreach (GameObject button in _buttons)
            {
                button.SetActive(false);
            }

            //Setting the animation for text game to disappear
            _canvasAnim.SetBool("visible", false);
        }
    }

    //Function called after text game disappear animation has completed
    private void ExitGame()
    {
        //Re-enabling standard interactions through the GameManager
         GameManager.instance._inTextGame = false;
    }

    //Function called by on-screen "Pause" UI button
    public void Pause()
    {
        _paused = !_paused;
    }

    private void PlayInteractSound()
    {
        //interact audio clip gets a randomized pitch for added variety
        _interactSound.pitch = Random.Range(0.8f, 1.1f);
        _interactSound.Play();
    }

    //If a text encounter has a unique audio clip, played here
    private void PlayEncounterSound(AudioClip _clip)
    {
        _encounterSound.clip = _clip;
        _encounterSound.Play();
    }
}
