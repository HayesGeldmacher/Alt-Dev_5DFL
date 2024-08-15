using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvidenceManager : MonoBehaviour
{
    [SerializeField] private int _picturesNeeded;
    [SerializeField] private Clapper _clapper;
    public List<GameObject> _evidenceBits = new List<GameObject>();
    public bool _hasEvidence = false;
    public bool _hasFoundPhone = false;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _textOption1;
    [SerializeField] private string _textOption2;
    [SerializeField] private Animator _textAnim;
    int _evidenceToRemove;
    private bool _shouldRemove = false;
    private bool _takenPic = false;
    [SerializeField] private bool _intro = false;
    [SerializeField] private List<GameObject> _items = new List<GameObject>();
    [SerializeField] private List<GameObject> _itemLabels = new List<GameObject>();
    private Dictionary<GameObject, GameObject> _itemsDict = new Dictionary<GameObject, GameObject>();
    [SerializeField] private AudioSource _phoneRing;
  


    private void Start()
    {
        for(int i = 0; i < _items.Count; i++)
        {
            _itemsDict.Add(_items[i], _itemLabels[i]);
        }
        Debug.Log(_itemsDict);
    }
    
    public void PictureTaken(GameObject evidence)
    {
        _picturesNeeded -= 1;
        Destroy(evidence);

        if (!_takenPic && _intro)
        {
           // _textAnim.SetBool("active", true);
          //  _text.text = "Damn, this old thing still works... I still need " + _picturesNeeded.ToString() + " more pictures...";
            transform.GetComponent<DialogueManager>().CallTimerEnd(2);
        }
        
        else if(_picturesNeeded <= 0)
        {
                    CompletePictures();
                 //   _textAnim.SetBool("active", true);
               //     _text.text = _textOption1;
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);
                }
                else if(_picturesNeeded == 1)
                {
               //     _textAnim.SetBool("active", true);
               //     _text.text = "I just need 1 more picture...";
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);
                }
                else
                {
               //     _textAnim.SetBool("active", true);
               //     _text.text = "I still need " + _picturesNeeded.ToString() + " more pictures...";
                    transform.GetComponent<DialogueManager>().CallTimerEnd(2);

                }
        

       
        _takenPic = true;
    }

    private void CompletePictures()
    {
        _hasEvidence = true;
        StartCoroutine(PhoneRing());
    }
    private void Phoned()
    {
        _hasFoundPhone = true;
    }

    private void PlaySound()
    {
        
    }

    public void StrikeOffItem(GameObject item)
    {
        GameObject currentLabel = _itemsDict[item];
        currentLabel.transform.GetChild(0).GetComponent<Animator>().SetTrigger("strike");
        Debug.Log(currentLabel + " Struck off");
        
    }

    private IEnumerator PhoneRing()
    {
        yield return new WaitForSeconds(2f);
        _phoneRing.Play();
    }
}
