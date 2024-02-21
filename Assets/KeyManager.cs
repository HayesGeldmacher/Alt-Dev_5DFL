using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public List<Key> _keys = new List<Key>();
    public List<Door> _doors = new List<Door>();

    Dictionary<Key, Door> _doorLocks = new Dictionary<Key, Door>();

    void Start()
    {
        int i = 0;
        foreach (var key in _keys)
        {
           _doorLocks.Add(key, _doors[i]);
            i++;
        }
        Debug.Log(_doorLocks.Count);
        
    }
}
