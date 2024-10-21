using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TextEncounter
{

    //if mustanswerall is true, there can be only one answer path, so set to 0
    //if mustanswerall is false, there can be multiple, so set to the numbers assoicated with correct answers
    //Set option to -1 in order to quit the game!

    public bool _interactive = true; 
   
    public List<int> _answerPaths;

    public bool _keepOption = false;
    public List<bool> _shouldKeepOption;

    [TextArea(3, 10)]
    public string _prompt;
    public List<string> _options;

    public int _finalPath;
    

}
