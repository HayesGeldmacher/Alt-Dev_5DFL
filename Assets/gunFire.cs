using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gunFire : MonoBehaviour
{
    [SerializeField] private Animator _gunAnim;
    private bool _isAiming = false;
    private bool _isShooting = false;
    [SerializeField] private AudioSource _gunShotSound;
    [SerializeField] private float _checkLength;
    [SerializeField] private LayerMask _checkMask;
    [SerializeField] private GameObject _gunArm;
    [SerializeField] private Animator _camAnim;

    [SerializeField] private GameObject _televisionCorpse;
    [SerializeField] private Animator _blackAnim;
    [SerializeField] private PauseButton _button;

    [SerializeField] private AudioSource _explode;

    //Raycast shit below:
    [SerializeField] private Transform _shootPoint;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _isAiming = true;
            _gunAnim.SetBool("aiming", true);

        }
        else
        {
            _isAiming = false;
            _gunAnim.SetBool("aiming", false);
        }

        if (_isAiming)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CallShot();
            }
        }

    }

    private void CallShot()
    {
        if (!_isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        _gunAnim.SetTrigger("shoot");
        _isAiming = false;
        _isShooting = true;
        _gunShotSound.Play();
        RayCastShoot();

        yield return new WaitForSeconds(1.5f);

        _isShooting = false;
    }


    private void RayCastShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_shootPoint.position, transform.forward, out hit, _checkLength, _checkMask))
        {
            if (hit.transform.tag == "television")
            {
                Debug.Log("GOT TV FUCK!");
                _explode.Play();
                Instantiate(_televisionCorpse, hit.transform.position, Quaternion.identity);
               Destroy(hit.transform.gameObject);
                _blackAnim.SetTrigger("death");
                Destroy(_gunArm);
                StartCoroutine(EndGame());
               
            }
          
        }
    }

    private IEnumerator EndGame()
    {

        yield return new WaitForSeconds(0.5f);
         _camAnim.SetTrigger("death");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MuseumViolence");
    }
}
