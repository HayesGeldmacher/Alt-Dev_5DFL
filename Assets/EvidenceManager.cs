using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvidenceManager : MonoBehaviour
{
    [SerializeField] private int _picturesNeeded;
    public List<GameObject> _evidenceBits = new List<GameObject>();
    public bool _hasEvidence = false;
    public bool _hasFoundPhone = false;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _textOption1;
    [SerializeField] private string _textOption2;
    [SerializeField] private Animator _textAnim;
    int _evidenceToRemove;
    private bool _shouldRemove = false;


    public void PictureTaken(GameObject evidence)
    {
        int i = 0;
        foreach(GameObject bit in _evidenceBits)
        {
                    
            Debug.Log("HEre!");
            if (bit.name == evidence.name)
            {
               
                _evidenceToRemove = i;
                _shouldRemove = true;
                _picturesNeeded -= 1;
                
            }
            i++;
                if(_picturesNeeded <= 0)
                {
                    CompletePictures();
                    _textAnim.SetBool("active", true);
                    _text.text = _textOption1;
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);
                }
                else
                {
                    _textAnim.SetBool("active", true);
                    _text.text = _textOption2;
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);
                }
        }
            if (_shouldRemove)
            {
            GameObject _removable = _evidenceBits[_evidenceToRemove];
             _evidenceBits.RemoveAt(_evidenceToRemove);
            Destroy(_removable);
                _shouldRemove = false;

            }
    }

    private void CompletePictures()
    {
        _hasEvidence = true;
    }
    private void Phoned()
    {
        _hasFoundPhone = true;
    }
}
