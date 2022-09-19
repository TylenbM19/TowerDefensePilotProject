using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _plane;

    private Player _player;

    private void Awake()
    {
        _player = Service.Instance.Get<Player>();
        _plane.SetActive(false);
    }

    private void Start()
    {
        _player.OnDie += OnDie;
    }

    private void OnDisable()
    {
        _player.OnDie -= OnDie;
    }

    private void OnDie()
    {    
        _plane.SetActive(true);
    }
}
