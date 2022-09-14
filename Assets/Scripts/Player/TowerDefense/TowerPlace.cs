using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlace : MonoBehaviour
{
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
        _tower = null;
    }

    private void ApplyObject(TowerDefense towerDefense)
    {
        _tower = towerDefense;
    }
}
