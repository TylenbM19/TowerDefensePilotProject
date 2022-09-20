using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _plane;

    private GameManager _player;

    private void Awake()
    {
        _player = Service.Instance.Get<GameManager>();
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
        Time.timeScale = 0;
        _plane.SetActive(true);
    }
}
