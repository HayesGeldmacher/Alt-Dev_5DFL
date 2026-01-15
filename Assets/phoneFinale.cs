using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class phoneFinale : Interactable
{
    private bool _finishedTalking = false;


    [SerializeField] private AudioSource _ringingSource;
    [SerializeField] private int _dialogueNum;
    [SerializeField] private AudioSource _phonePickupSound;
    [SerializeField] private AudioSource _phonePutDownSound;

    [SerializeField] private bool _pickedUp = false;
    [SerializeField] private bool _finished = false;

    [SerializeField] private GameObject _wallDisappear;
    [SerializeField] private Dialogue _dialogueEnd;

    [SerializeField] private CameraController _camController;
    public Animator phoneAnim;

    [SerializeField] private GameObject[] disappearObjects;

    [Header("Darkness Fields")]
    public bool isDarkening = false;
    private float t;
    public Color colorStart;
    public Color colorEnd;
    public float duration;
    public Light light;
    public Color lightColorStart;
    float startIntensity = 0.65f;
    public float desiredIntensity = 0.22f;
    float startReflection;

    [Header("AudioFields")]
    public AudioSource staticSound;
    public float fadeSpeed = 0.05f;
    private bool fadingUp = false;
    // Start is called before the first frame update
    void Start()
    {
        phoneAnim.SetTrigger("on");
        t = 0;
        
        //just for testing 
        // StartCoroutine(StartDark());

    }

    // Update is called once per frame
    void Update()
    {
        if (isDarkening)
        {

            Color lerpedColor = Color.Lerp(colorStart, colorEnd, t);
            RenderSettings.skybox.SetColor("_Tint", lerpedColor);

            Color lightLerpedColor = Color.Lerp(lightColorStart, colorEnd, t);
            light.color = lightLerpedColor;

            float lerpedIntensity = Mathf.Lerp(startIntensity, desiredIntensity, t);
            RenderSettings.ambientIntensity = lerpedIntensity;

            float lerpedReflection = Mathf.Lerp(startReflection, 0, t);

            t += Time.deltaTime / duration;

        }

        float oldVolume;
        if (fadingUp)
        {
           if(staticSound.volume >= 0.25f)
            {
                fadingUp = false;
                return;
            }

            
            oldVolume = staticSound.volume;
            float newVolume = oldVolume + (fadeSpeed * Time.deltaTime);
            staticSound.volume = newVolume;
        }
    }

    public override void Interact()
    {


        if (_finished)
        {
            base._dialogue = _dialogueEnd;
            base.Interact();
        }
        else
        {
            base.Interact();

            if (!_pickedUp)
            {
                _phonePickupSound.Play();
                phoneAnim.SetTrigger("talking");
                _pickedUp = true;
                _ringingSource.Stop();
                PlayerController.instance._frozen = true;
               // _camController._frozen = true;
                
            }

            _dialogueNum -= 1;

            if(_dialogueNum <= 0)
            {
                EndPhoneCall();
            }
        }
    }

    private void EndPhoneCall()
    {
        _finished = true;
        phoneAnim.SetTrigger("off");
        _phonePutDownSound.Play();
        _wallDisappear.SetActive(false);
        PlayerController.instance._frozen = false;
        _camController._frozen = false;
        foreach (GameObject disappearObject in disappearObjects){

            if (disappearObject != null){ 
                disappearObject.SetActive(false);
            }
        }
        StartCoroutine(StartDark());
    }

    private IEnumerator StartDark()
    {
        isDarkening = true;
        colorStart = RenderSettings.skybox.GetColor("_Tint");
        lightColorStart = light.color;
        startIntensity = RenderSettings.ambientIntensity;
        startReflection = RenderSettings.reflectionIntensity;
        fadingUp = true;
        staticSound.Play();
        yield return new WaitForSeconds(1f);
    }

    
}
