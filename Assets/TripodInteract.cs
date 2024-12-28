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
        base.Interact();
        _treeAnim.SetTrigger("transform");
        if (_treeSound != null) 
        { 
            _treeSound.Play();
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

            GetComponent<MeshRenderer>().enabled = false; 
            StartCoroutine(AnimEnd());

        }
        
    }

    private IEnumerator AnimEnd()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
