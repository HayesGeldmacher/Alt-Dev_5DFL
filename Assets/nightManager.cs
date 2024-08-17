using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nightManager : MonoBehaviour
{

    
    [SerializeField] private PlayerController _controller;
    [SerializeField] private List<GameObject> _Items = new List<GameObject>();
    [SerializeField] private int _itemNum;
    

    // Start is called before the first frame update
    void Start()
    {
        _controller._frozen = true;
        _itemNum = 0;

        float _countNum = 0;
        foreach(var item in _Items)
        {
            if(_countNum != 0)
            {
                item.SetActive(false);
            }

            _countNum ++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextItem()
    {
       
        
        _itemNum++;

        if(_itemNum < _Items.Count)
        {
        _Items[_itemNum].SetActive(true);

        }
    }
}
