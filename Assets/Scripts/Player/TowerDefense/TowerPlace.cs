using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    [SerializeField] private TowerDefense _tower;

    private void OnEnable()
    {
        PlayerWindow.OnTower += ApplyObject;
    }

    private void OnDisable()
    {
        PlayerWindow.OnTower -= ApplyObject;
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
