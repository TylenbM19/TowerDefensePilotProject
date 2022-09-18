using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject _plane;

    private void Awake()
    {
        _plane.SetActive(false);
    }

    private void Start()
    {
        Player.OnDie += OnDie;
    }

    private void OnDisable()
    {
        Player.OnDie -= OnDie;
    }

    private void OnDie()
    {    
        _plane.SetActive(true);
    }
}
