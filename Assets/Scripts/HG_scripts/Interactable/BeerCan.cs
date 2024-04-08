using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerCan : Interactable
{
    private Rigidbody _rb;
    [SerializeField] private float _strength;
    private AudioSource _canHit;
    [SerializeField] private bool _canSound;
    [SerializeField] private float _soundWait;
    [SerializeField] private float _currentWait;

    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _canHit = GetComponent<AudioSource>();
        _canSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_canSound)
        {
            _currentWait -= Time.deltaTime;
            if( _currentWait <= 0)
            {
                _canSound = true;
            }
        }
    }

    public override void Interact()
    {
        float _velX = Random.Range(_strength * -1.5f, _strength * 1.5f);
        float _velY = Random.Range(_strength * 0.5f, _strength * 2f);
        float _velZ = Random.Range(_strength * -1.5f, _strength * 1.5f);
        Vector3 _beerCanVel = new Vector3(_velX, _velY, _velZ);
        _rb.velocity += (_beerCanVel);

        _rb.AddTorque(transform.up * 160);
    }

     void OnCollisionEnter(Collision other)
    {

        if (_canHit)
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        _canSound = false;
        _currentWait = _soundWait;
        _canHit.pitch = Random.Range(0.7f, 1.3f);
        _canHit.Play();

    }
}
