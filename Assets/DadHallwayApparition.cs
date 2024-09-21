using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadHallwayApparition : MonoBehaviour
{
    [SerializeField] private GameObject _bloodCloud;
    [SerializeField] private Transform _cloudSpawnPos;

    [SerializeField] private Animator _lightAnim;

    [SerializeField] private GameObject _dadMesh;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCloud()
    {
        GameObject _newCloud = GameObject.Instantiate(_bloodCloud, _cloudSpawnPos.position, _cloudSpawnPos.rotation);

    }


    public void KillDad()
    {
        StartCoroutine(KillDadSequence());
    }

    private IEnumerator KillDadSequence()
    {
        _lightAnim.SetTrigger("off");
        yield return new WaitForSeconds(0.3f);
         Destroy(_dadMesh);
        yield return new WaitForSeconds(3f);
        _lightAnim.SetTrigger("on");
    }

}
