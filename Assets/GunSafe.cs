using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSafe : MonoBehaviour
{
    [SerializeField] private string _neededCode;
    public string _currentCode;
    [SerializeField] private List<int> _currentNumbers = new List<int>();
    [SerializeField] private Animator _anim;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private TMP_Text _displayText;
    private bool _open = false;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<AudioClip> _buttonClicks = new List<AudioClip>();
    private int _count = 0;

    [SerializeField] private AudioSource _successSound;
    [SerializeField] private AudioSource _failureSound;

    [SerializeField] private Animator _textAnim;

    // Start is called before the first frame update
    void Start()
    {
        _displayText.text = _currentCode.ToString();
    }

    // Update is called once per frame
    void Update()
    {
          

    }

    private IEnumerator Unlock()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("OPENED!");
        _anim.SetTrigger("open");
        _boxCollider.enabled = false;
        _open = true;
    }

    public void ButtonPressed( int _buttonPressed)
    {
        _currentNumbers.Add(_buttonPressed);
        _currentCode = "";
        foreach (int i in _currentNumbers)
        {
            _currentCode += i.ToString();
        }
        _displayText.text = _currentCode.ToString();

        PlaySound();

        if (_currentNumbers.Count >= 4)
        {
            //check here if correct or false!

            if (_currentCode == _neededCode)
            {
                if (!_open)
                {
                    StartCoroutine(CheckCombination(true));
                }
            }
            else
            {
                StartCoroutine(CheckCombination(false));
            }
           
        }
    }

    private IEnumerator CheckCombination(bool _correct)
    {
        yield return new WaitForSeconds(0.1f);

        if (_correct)
        {
            _successSound.Play();
            _textAnim.SetTrigger("success");
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(Unlock());
        }
        else
        {

            //play a negative sound here!
            _failureSound.Play();
            _textAnim.SetTrigger("fail");
            yield return new WaitForSeconds(0.4f);
            Debug.Log("Failed");
        }

        _currentNumbers.Clear();
        _currentCode = "";
        _displayText.text = _currentCode;
    }

    private void PlaySound()
    {
        //playing sequenced sounds from list
        if (_count >= _buttonClicks.Count)
        {
            _count = 0;
        }
        _audio.clip = _buttonClicks[_count];
        _audio.pitch = Random.Range(0.8f, 1.2f);
        _audio.Play();
        _count++;
    }
}
