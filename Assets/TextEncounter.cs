using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TextEncounter
{

    //if mustanswerall is true, there can be only one answer path, so set to 0
    //if mustanswerall is false, there can be multiple, so set to the numbers assoicated with correct answers

    public bool _interactive = true; 
   
    public List<int> _answerPaths;

    [TextArea(3, 10)]
    public string _prompt;
    public List<string> _options;

    public int _finalPath;
    

}
