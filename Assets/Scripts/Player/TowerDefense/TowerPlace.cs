using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    [SerializeField] private GameObject _container;

    private TowerDefense _tower;

    private void OnEnable()
    {
        Service.Instance.Get<PlayerWindow>().OnTower += ApplyObject;
    }

    private void OnDisable()
    {
        Service.Instance.Get<PlayerWindow>().OnTower -= ApplyObject;
    }

    private void OnMouseDown()
    {
        TowerDefense currentDefense = Instantiate(_tower, transform.position, Quaternion.identity);
        currentDefense.SetContainer(_container);
        _tower = null;
    }

    private void ApplyObject(TowerDefense towerDefense)
    {
        _tower = towerDefense;
    }


}
