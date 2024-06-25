using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelTriggerCounter : MonoBehaviour
{
    [SerializeField] private museumTunnelNature _masterTunnel;
    [SerializeField] private bool _hasActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        _hasActivated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!_hasActivated)
            {
                _masterTunnel.AddTunnel();
                _hasActivated = true;
                Debug.Log("TunnelAdded!");
            }
        }
    }
}
