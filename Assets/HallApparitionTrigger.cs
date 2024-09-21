using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallApparitionTrigger : MonoBehaviour
{
    [SerializeField] private Animator _dadAnim;
    private bool _triggered = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator PlaySequence()
    {
        _dadAnim.SetTrigger("fire");
        yield return new WaitForSeconds(0.1f);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_triggered)
            {
                _triggered = true;
                StartCoroutine(PlaySequence());
            }
        }
    }

}
