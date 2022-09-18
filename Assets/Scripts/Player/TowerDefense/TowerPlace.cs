using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    private TowerDefense _tower;
    private Player _player;

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
            _player.DisableBuild();
        }
    }

    private void ApplyObject(TowerDefense? towerDefense)
    {
        _tower = towerDefense;
    }

    private void Instantiate()
    {
        Instantiate(_tower, transform.position, Quaternion.identity);
        _player.Bye(_tower.Price);
        _tower = null;
    }
}
