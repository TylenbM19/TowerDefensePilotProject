using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private T _prefab;

    public int Count => _pool.Count;

    private List<T> _pool = new List<T>();

    public Pool(T prefab)
    {
        _prefab = prefab;
    }
    public T Get()
    {
        return GetDisable() ?? Create();
    }

    private  T GetDisable()
    {
        if (_pool.Count == 0)
            return null;

        foreach(T pool in _pool)
        {
            if(pool.gameObject.activeSelf == false)
                return pool;
        }
        return null;
    }

    private T Create()
    {
        T spawned = GameObject.Instantiate(_prefab);
        _pool.Add(spawned);
        return spawned;
    }
}
