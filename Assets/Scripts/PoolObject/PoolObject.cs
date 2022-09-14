using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    protected int Count => _pool.Count;

    private GameObject _container;
    private List<GameObject> _pool = new List<GameObject>();

    public void SetContainer(GameObject container)
    {
        _container = container;
    }

    protected GameObject GetObject()
    {
        GameObject result = AddObject();

        result = _pool.First(p => p.activeSelf == false);

        if (result == null)
            result = AddObject();

        return result;
    }

    private GameObject AddObject()
    {
        GameObject spawned = Instantiate(_prefab, _container.transform);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }

}
