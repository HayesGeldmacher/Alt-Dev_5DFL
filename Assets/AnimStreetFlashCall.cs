using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStreetFlashCall : MonoBehaviour
{

    [Header("Anim Fields")]
    public float _minCallTime;
    public float _maxCallTime;
    private float _currentCallTime;

    public Animator _anim;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _currentCallTime = Random.Range(_minCallTime, _maxCallTime);
    }

    // Update is called once per frame
    void Update()
    {
        _currentCallTime -= Time.deltaTime;
        if(_currentCallTime <= 0)
        {
            CallAnim();
        }
    }

    private void CallAnim()
    {
        _currentCallTime = Random.Range(_minCallTime, _maxCallTime);
        _anim.SetTrigger("flash");
    }
}
