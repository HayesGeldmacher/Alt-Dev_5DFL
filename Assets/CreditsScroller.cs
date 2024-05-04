using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroller : MonoBehaviour
{ 
    [SerializeField] private Transform _cam;
    [SerializeField] private float _speed;
    [SerializeField] private float _stopPoint;
    [SerializeField] private float _doubleSpeed;
    [SerializeField] private Transform _credits;
    private bool _scrolling = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCredits());
    }

    // Update is called once per frame
    void Update()
    {
        if(_scrolling)
        {
            if (Input.GetMouseButton(0))
            {
                _credits.Translate(Vector3.up * Time.deltaTime * _doubleSpeed);
            }
            else if (Input.GetMouseButton(1))
            {
               
            }
            else
            {
            _credits.Translate(Vector3.up * Time.deltaTime * _speed);

            }

            Vector3 cameraRelative = _cam.InverseTransformPoint(_credits.position);
            Debug.Log(cameraRelative);

            if(cameraRelative.y == _stopPoint)
            {
                _scrolling = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }


    private IEnumerator StartCredits()
    {
        yield return new WaitForSeconds(4f);
        Debug.Log("STARTED");
        _scrolling = true;

    }
}
