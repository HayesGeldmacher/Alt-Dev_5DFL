using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : Interactable
{
    public bool _entered = false;
    public float _enterTotalTime;
    public float _enterCurrentTime;
    public bool _canExit = false;
    [SerializeField] private Animator _telescopeAnim;
    public BoxCollider _collider;
    public CameraController camController;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();
        if (_entered)
        {
            _enterCurrentTime -= Time.deltaTime;
            if (_enterCurrentTime <= 0)
            {
                _canExit = true;
            }

            if (Input.GetButtonDown("Interact"))
            {
                if (_canExit)
                {
                    StartCoroutine(ExitTelescope());
                }
            }
        }
    }

    public override void Interact()
    {
        if (!_entered)
        {
            base.Interact();
            _entered = true;
            EnterTelescope();
        }
    }
    
    private void EnterTelescope()
    {
        Debug.Log("ENTERED TELESCOPE");
        _collider.enabled = false;
        GameManager.instance.FreezePlayer(true);
        _enterCurrentTime = _enterTotalTime;
        _telescopeAnim.SetTrigger("telescope");
    }

    private IEnumerator ExitTelescope()
    {
        if (camController != null) { 
        camController.PlayInteractAudio();
        
        }
        else
        {
            Debug.LogWarning("CAM CONTROLLER REF MISSING IN TELESCOPE!");
        }

            GameManager.instance.FreezePlayer(false);
        _telescopeAnim.SetTrigger("disappear");
        yield return new WaitForSeconds(0.2f);
        _collider.enabled = true;
        _entered = false;
        _canExit = false;
    }

}
