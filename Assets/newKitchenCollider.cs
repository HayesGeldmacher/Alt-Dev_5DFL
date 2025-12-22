using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newKitchenCollider : Interactable
{

    [SerializeField] private NightmareTriggerKitchen _colFlag1;
    [SerializeField] private NightmareTriggerKitchen _colFlag2;
    [SerializeField] private NightmareTriggerKitchen _currentFlag;
    [SerializeField] private nightManager _nightManage;
    public AudioSource spotlightAudio;
    private bool _canTrigger = true;

    public bool _enableExit = false;
    public GameObject[] appearObjects;
    public GameObject[] disappearObjects;
    private bool hasEnabledExit = false;

    [Header("Exit Fields")]
    public bool hasEnded = false;
    public bool spawnWalls = false;
    public GameObject fakeWallExitRight;
    public GameObject fakeWallExitLeft;
    public bool spawnLeft = false;
    public NightmareKitchenManager manager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

        if(other.tag == "Player")
        {

            if (_canTrigger)
            {
                _canTrigger = false;
                StartCoroutine(TriggerWait());
                //_nightManage.AddRoom(this);

                spotlightAudio.Play();
                if(_colFlag1 == _currentFlag)
                {
                    _colFlag1.StartFade();
                    _colFlag2.StartAppear();


                    if (spawnWalls)
                    {
                        if (manager.hasEnded)
                        {
                            fakeWallExitLeft.SetActive(spawnLeft);
                            fakeWallExitRight.SetActive(!spawnLeft);
                        }
                    }

                    
                }
                else
                {
                    _colFlag1.StartAppear();
                    _colFlag2.StartFade();

                    if (spawnWalls)
                    {
                        if (manager.hasEnded)
                        {
                            fakeWallExitLeft.SetActive(!spawnLeft);
                            fakeWallExitRight.SetActive(spawnLeft);
                        }
                    }

                }

                if (_enableExit)
                {

                    if (!hasEnabledExit && !manager.hasEnded)
                    {
                        hasEnabledExit = true;
                        manager.hasEnded = true;
                        foreach (GameObject _object in appearObjects){
                            _object.SetActive(true);
                        }
                        foreach (GameObject _object in disappearObjects)
                        {
                            _object.SetActive(false);
                        }
                    }
                    
                }
            }

        }
            
    }


    public void SetChildFlag(NightmareTriggerKitchen _passedFlag)
    {
        _currentFlag = _passedFlag;
    }

    private IEnumerator TriggerWait()
    {
        yield return new WaitForSeconds(0.1f);
        _canTrigger = true;
    }

    
}
