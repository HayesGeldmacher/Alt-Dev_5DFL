using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinaleDreamEnd : Interactable
{

    [SerializeField] private Animator _whiteOutAnim;
    [SerializeField] private Animator _camAnim;
    [SerializeField] private Animator _doorAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter()
    {
        base.Interact();
        StartCoroutine(EndScene());
    }

    private IEnumerator EndScene()
    {
        
        yield return new WaitForSeconds(20f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
