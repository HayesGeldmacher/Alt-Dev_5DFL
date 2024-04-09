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
        _picturesNeeded -= 1;
        Destroy(evidence);
                if(_picturesNeeded <= 0)
                {
                    CompletePictures();
                    _textAnim.SetBool("active", true);
                    _text.text = _textOption1;
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);
                }
                else if(_picturesNeeded == 1)
                {
                    _textAnim.SetBool("active", true);
                    _text.text = "I just need 1 more picture...";
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);
                }
                else
                {
                    _textAnim.SetBool("active", true);
                    _text.text = "I still need " + _picturesNeeded.ToString() + " more pictures...";
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);

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

    private void PlaySound()
    {
        
    }
}
