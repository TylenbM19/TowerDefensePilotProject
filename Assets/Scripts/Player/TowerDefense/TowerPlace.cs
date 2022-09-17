using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerPlace : MonoBehaviour
{
    private TowerDefense _tower;
    private Player _player;

    public event UnityAction<bool> OnReset;

    private void Start()
    {
        _player = Service.Instance.Get<Player>();
        _player.OnTower += ApplyObject;
    }

    private void OnDisable()
    {
        _player.OnTower -= ApplyObject;
    }

    private void OnMouseDown()
    {
        if (_tower != null)
        {
            Instantiate();
        }
    }

    private void ApplyObject(TowerDefense towerDefense)
    {
        _tower = towerDefense;
    }

    private void Instantiate()
    {
        Instantiate(_tower, transform.position, Quaternion.identity);
        _player.Bye(_tower.Price);
        OnReset?.Invoke(true);
    }
}
