using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorTrigger : MonoBehaviour
{
    public List<Transform> disappearList = new List<Transform>();
    private bool _visible = true;
    public bool _canTouch;
    float _playerDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        _canTouch = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Appear()
    {
        Debug.Log("Appeared!");
        
        if (_playerDistance > 0)
        {
            _visible = false;

            foreach(Transform obj in disappearList)
            {
                
                if(obj != null)
                {
                    if(obj.GetComponent<MeshRenderer>() != null)
                    {
                    obj.GetComponent<MeshRenderer>().enabled = false;
                    }

                    if(obj.GetComponent<SpriteRenderer>() != null)
                    {
                        obj.GetComponent<SpriteRenderer>().enabled = false;
                    }

                    if(obj.childCount > 0)
                    {
                        RecursiveCheck(obj, false);

                    }

                }
                
                

            }
        }
        else
        {

            foreach (Transform obj in disappearList)
            {
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().enabled = true;
                }

                if (obj.GetComponent<SpriteRenderer>() != null)
                {
                    obj.GetComponent<SpriteRenderer>().enabled = true;
                }

                if (obj.childCount > 0)
                {
                    RecursiveCheck(obj, true);
                }

            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Appear();
            _canTouch = true;
            _playerDistance = transform.position.z - other.transform.position.z;
            Debug.Log(_playerDistance);
        }

    }

    private void RecursiveCheck(Transform parent, bool appear)
    {

        if(appear)
        {

        foreach (Transform child in parent)
            {
                if (child.GetComponent<MeshRenderer>() != null)
                {
                    child.GetComponent<MeshRenderer>().enabled = true;
                }

                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                }

                if(child.childCount > 0)
                {
                    RecursiveCheck(child, true);
                }
            }
        }
        else
        {
            foreach (Transform child in parent)
            {
                if (child.GetComponent<MeshRenderer>() != null)
                {
                    child.GetComponent<MeshRenderer>().enabled = false;
                }

                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    child.GetComponent<SpriteRenderer>().enabled = false;
                }

                if (child.childCount > 0)
                {
                    RecursiveCheck(child, false);
                }
            }
        }
        


    }
}
