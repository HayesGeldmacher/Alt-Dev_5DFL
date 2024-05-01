using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{

    [SerializeField] private GameObject _mesh;
    [SerializeField] private GameObject _bloodRed;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(DisappearMesh());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DisappearMesh()
    {
        yield return new WaitForSeconds(1f);
        _bloodRed.SetActive(true);
        Animator _anim = _bloodRed.transform.GetComponent<Animator>();
        _anim.SetTrigger("breathe");
        _mesh.SetActive(false);
        yield return new WaitForSeconds(1f);
        _anim.SetTrigger("stop");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
