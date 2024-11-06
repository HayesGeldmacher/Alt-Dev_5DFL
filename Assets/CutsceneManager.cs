using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{

    [SerializeField] private Animator _doorAnim;
    [SerializeField] private Animator _clickAnim;
    private bool _canStart = false;
    private bool _hasStarted = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_hasStarted)
            {
                _hasStarted = true;
                StartSequence();
            }
        }
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(1.5f);
        if (!_hasStarted)
        {
            _canStart = true;
        }
    }

    private void StartSequence()
    {
        _doorAnim.SetTrigger("start");
        _clickAnim.SetTrigger("click");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
