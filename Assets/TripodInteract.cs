using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodInteract : Interactable
{

    [SerializeField] private Animator _treeAnim;
    [SerializeField] private AudioSource _treeSound;
    [SerializeField] private AudioSource _endInteractSound;

    [SerializeField] private GameObject[] _disappearObjects;
    [SerializeField] private GameObject[] _appearObjects;

    [SerializeField] private CameraController _camController;

 

    private bool _startedEnd = false;
    private bool _playedAnimatiton = false;

    // Start is called before the first frame update
    private void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Interact()
    {

        if (!_playedAnimatiton)
        {
            _playedAnimatiton = true;
            PlayerController.instance._frozen = true;
            _camController._canInteract = false;
            _camController._frozen = true;
            transform.GetComponent<BoxCollider>().enabled = false;
            _treeAnim.SetTrigger("transform");
            if (_treeSound != null) 
            { 
                _treeSound.Play();
            }
        }
        else
        {
            base.Interact();
        }
        


    }


    public void CallAnimEnd()
    {
       

        if (!_startedEnd)
        {
            

            _startedEnd = true;
            if (_endInteractSound != null) 
            { 
                _endInteractSound.Play();  
            }


            foreach (var obj in _disappearObjects)
            {
                obj.SetActive(false);
            }

            foreach (var obj in _appearObjects)
            {
                obj.SetActive(true);
            }

            _treeAnim.SetTrigger("end");

            StartCoroutine(AnimEnd());

        }
        
    }

    private IEnumerator AnimEnd()
    {
        yield return new WaitForSeconds(1f);
        _camController._frozen = false;
        PlayerController.instance._frozen = false;
        _camController._canInteract = true;
        transform.GetComponent<BoxCollider>().enabled = true;

    }
}
