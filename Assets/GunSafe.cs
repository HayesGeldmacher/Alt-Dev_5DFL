using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunSafe : MonoBehaviour
{
    [SerializeField] private string _neededCode;
    public string _currentCode;
    [SerializeField] private Animator _anim;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private TMP_Text _displayText;
    private bool _open = false;

    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<AudioClip> _buttonClicks = new List<AudioClip>();
    private int _count = 0;

    // Start is called before the first frame update
    void Start()
    {
        _displayText.text = _currentCode.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       if(_currentCode == _neededCode)
        {
            if (!_open)
            {
                Unlock();
            }
        }
    }

    private void Unlock()
    {
        Debug.Log("OPENED!");
        _anim.SetTrigger("open");
        _boxCollider.enabled = false;
        _open = false;
    }

    public void ButtonPressed( int _buttonPressed)
    {
        Debug.Log(_buttonPressed);
        
        
        
        //playing sequenced sounds fromt list
        if (_count > _buttonClicks.Count)
        {
            _count = 0;
        }
        _audio.clip = _buttonClicks[_count];
        _audio.pitch = Random.Range(0.8f, 1.2f);
        _audio.Play();
        _count++;
    }
}
