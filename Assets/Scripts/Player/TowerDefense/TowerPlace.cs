using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    private TowerDefense _tower = null;
    private Player _player;

    private void Start()
    {
        _player = Service.Instance.Get<Player>();
    }

    public void TryConstruct(TowerDefense towerDefense)
    {
        if (_tower == null && _player.CurrentEnergy >= towerDefense.Price)
        {
            _tower = Instantiate(towerDefense, transform.position, Quaternion.identity);
            _player.Bye(_tower.Price);
            _tower = null;
        }
    }
}
