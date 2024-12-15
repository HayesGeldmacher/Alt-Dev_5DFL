using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;
using System.Linq;

public class TextEncounterManager : MonoBehaviour
{
    ///This script handles the main logic for a text adventure game including:
    /// Branching text encounters according to player input
    /// Altering UI elements inlcuding dialogue and buttons

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

        for (int i = 0; i < _buttons.Count(); i++)
        {
            Button currentButton = _buttons[i].GetComponent<Button>();
            currentButton.onClick.AddListener(() => ChooseOption(i));
        }

        //Initiating the first TextGame encounter
        SetEncounter(0);
    }

    //Called by above OptionSelected functions
    public void ChooseOption(int option)
    {
        //Only process input if game is unpaused and currently being played
        if (_ended || _paused) return;

        //Checks if there are still available dialogue options in current text encounter
        if (_currentEncounter._options.Count > 0)
        {
            //Keeping previous text encounter saved as local variable before moving to next encounter
            TextEncounter oldEncounter = _currentEncounter;

            //Moving to the next text encounter determined by chosen dialogue option
            SetEncounter(_currentEncounter._answerPaths[option]);

            //Checking if the previous text encounter should have chosen dialogue paths kept
            if (oldEncounter._keepOption)
            {
                //Checking if particular chosen option should be kept
                bool _shouldKeepOption = oldEncounter._shouldKeepOption[option];

                //If chosen option doesn't keep, remove both chosen option and associated answer path
                if (!_shouldKeepOption)
                {
                    oldEncounter._options.RemoveAt(option);
                    oldEncounter._answerPaths.RemoveAt(option);
                }
            }
            //If previous encounter doesn't keep options, remove both chosen option and associated answer path
            else
            {
                oldEncounter._options.RemoveAt(option);
                oldEncounter._answerPaths.RemoveAt(option);
            }
        }
        //If no available dialogue options, move to next text encounter
        else
        {
            SetEncounter(_currentEncounter._finalPath);
        }

        PlayInteractSound();
    }

    private void SetEncounter(int nextEncounterNum)
    {
        //Setting text encounter final path to -1 quits the game
        if (nextEncounterNum == -1) { DisableGame(); return;}

        //Creating local variable for the next chosen text encounter
        TextEncounter _newEncounter = _encounterList[nextEncounterNum];

        //If new text encounter has no dialogue options, skip to following text encounter
        if (_newEncounter._interactive && _newEncounter._options.Count <= 0)
        {
            _newEncounter = _encounterList[_newEncounter._finalPath];
        }

        _currentEncounter = _newEncounter;

        //Deteterming how many dialogue options in new text encounter
        Debug.Log("Current Options: " + _currentEncounter._options.Count);

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
        if (_currentEncounter._hideOptions)
        {
            _optionTexts[0].text = "";
            _blackBoxes[0].SetActive(false);
            _promptBox.SetActive(false);
            _buttons[0].SetActive(false);
            return;
        }

        // Set as many boxes as there are options to be active, the rest to be inactive
        for (int i = 0; i < _blackBoxes.Count(); i++)
        {
            bool active = i < _currentEncounter._options.Count;

            _optionTexts[i].text = (i + 1) + ": " + _currentEncounter._options[i];
            _blackBoxes[i].SetActive(active);
            _buttons[i].SetActive(active);
        }
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

