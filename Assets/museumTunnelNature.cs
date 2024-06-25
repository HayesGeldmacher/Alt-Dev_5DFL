using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class museumTunnelNature : MonoBehaviour
{

    public int tunnelID = 0;
    [SerializeField] private GameObject _tunnel1;
    [SerializeField] private GameObject _tunnel2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTunnel()
    {
        tunnelID++;
        if(tunnelID >= 2 ) 
        { 
            SpawnTunnels();
        }
    }

    private void SpawnTunnels()
    {
        _tunnel1.SetActive(false);
        _tunnel2.SetActive(false);
    }
}
