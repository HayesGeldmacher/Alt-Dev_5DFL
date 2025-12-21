using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneShootTrigger : ShootTrigger
{

    public GameObject[] appearObjects;
    public GameObject[] disappearObjects;
    public AudioSource _dingAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void Interact()
    {

        transform.GetComponent<BoxCollider>().enabled = false;
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(PlayDing());

        foreach (GameObject obj in appearObjects) {

            if (obj != null) { 
                
                obj.SetActive(true);
            
            }
        }

        foreach (GameObject obj in disappearObjects)
        {

            if (obj != null)
            {

                obj.SetActive(false);

            }
        }



    }


    IEnumerator PlayDing()
    {
        Debug.Log("PLAYED DING!");
        yield return new WaitForSeconds(0.5f);
        if(_dingAudio != null)
        {
            _dingAudio.Play();
        }
        Destroy(gameObject);
    }
}
