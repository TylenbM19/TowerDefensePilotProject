using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    private Tower _tower = null;
    private GameManager _player;

    private void Start()
    {
        _player = Service.Instance.Get<GameManager>();
    }

    public void TryConstruct(Tower towerDefense)
    {
        if (_tower == null && _player.CurrentEnergy >= towerDefense.Price)
        {
            _tower = Instantiate(towerDefense, transform.position, Quaternion.identity);
            _player.Bye(_tower.Price);
            _tower = null;
        }
    }
}
