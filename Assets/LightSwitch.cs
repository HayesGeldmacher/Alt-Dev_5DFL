using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LightSwitch : Interactable
{
    [SerializeField] private Animator _switchAnim;
    [SerializeField] private CeilingLight _ceilingLight;
    public bool _on = false;
    private AudioSource _click;

    [Header("GARAGE SHIT")]
    [SerializeField] private bool _isGarage = false;
    [SerializeField] private GameObject _GarageBulb;


    [Header("disappear SHIT")]
    public bool manageItems = false;
    public GameObject[] appearItems;
    public GameObject[] disappearItems;

    // Start is called before the first frame update
    void Start()
    {
        //_on = false;
        _click = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        _click.Play();

        
        if (_on)
        {
            _on = false;
            _switchAnim.SetBool("on", false);

            if (_isGarage)
            {
                _GarageBulb.SetActive(false);
            }
            else
            {
                
            _ceilingLight.Flip(false);

                
            }

            if (manageItems)
            {
                foreach(GameObject obj in appearItems)
                {
                    if(obj != null)
                    {
                        obj.SetActive(true);
                    }
                }

                foreach(GameObject obj in disappearItems)
                {
                    if(obj != null)
                    {
                        obj.SetActive(false);
                    }
                }

            }
        }
        else
        {
           
            _on = true;
            _switchAnim.SetBool("on", true);
            
            if (_isGarage)
            {
                _GarageBulb.SetActive(true);
            }
            else
            {
            _ceilingLight.Flip(true);

            }    

        }    
    }
}
