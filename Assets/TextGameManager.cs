using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGameManager : MonoBehaviour
{

    [Header("TextVariables")]
    [SerializeField] private TMP_Text _promptText;
    [SerializeField] private TMP_Text _optionText1;
    [SerializeField] private TMP_Text _optionText2;
    [SerializeField] private TMP_Text _optionText3;
    
    [SerializeField] private int _currentOptions;
    [SerializeField] private int _currentPassage;

    [SerializeField] private TextEncounter _currentEncounter;
    public TextEncounter[] _encounterList;



    //if you must select all options, set _allOptionsList to 3

    //if one option is correct


    // Start is called before the first frame update
    void Start()
    {
        _currentPassage = 0;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _currentOptions > 0)
        {
            ChooseOption(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && _currentOptions > 1)
        {
            ChooseOption(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && _currentOptions > 2)
        {
            ChooseOption(2);
        }
        else if(Input.anyKeyDown && _currentOptions <= 0)
        {
            ChooseOption(0);
        }
       
    }

    public void StartGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        SetEncounter(0);
    }

    public void ChooseOption(int _option)
    {

        if( _currentOptions > 0 )
        {
         TextEncounter _oldEncounter = _currentEncounter;
        SetEncounter(_currentEncounter._answerPaths[_option]);
        _oldEncounter._options.RemoveAt(_option);
        _oldEncounter._answerPaths.RemoveAt(_option);

        }
        else
        {
            SetEncounter(_currentEncounter._finalPath);
        }
        

    }

    private void SetEncounter(int _nextEncounterNum)
    {

       
            
         TextEncounter _newEncounter = _encounterList[_nextEncounterNum];

        if(_newEncounter._interactive && _newEncounter._options.Count <= 0)
        {
            _newEncounter = _encounterList[_newEncounter._finalPath];
        }
        _currentEncounter = _newEncounter;

        Debug.Log("CURRENTOPTIONS: " + _currentEncounter._options.Count);
        _currentOptions = _currentEncounter._options.Count;

        _promptText.text = _currentEncounter._prompt;


        if(_currentOptions > 0)
        {
            _optionText1.text = "1: " + _currentEncounter._options[0];

            if(_currentOptions > 1)
            {
                _optionText2.text = "2: " + _currentEncounter._options[1];

                if (_currentOptions > 2)
                {
                    _optionText3.text = "3: " + _currentEncounter._options[2];
                }
                else
                {
                    _optionText3.text = "";
                }
            }
            else
            {
                _optionText2.text = "";
                 _optionText3.text = "";
            }
        }
        else
        {
            _optionText1.text = "Any Key To Continue";
            _optionText2.text = "";
            _optionText3.text = "";
        }

        
       

    }
}
