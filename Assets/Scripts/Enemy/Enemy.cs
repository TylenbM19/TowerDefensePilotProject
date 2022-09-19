using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private ParticleSystem _particleSystem;

    public static event UnityAction<int> ConsumeReward;
    public static event UnityAction<int> Damage;
    public event UnityAction<Enemy> Died;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth += damage;

        if (_currentHealth <= 0)
        {
            Die();
            ConsumeReward?.Invoke(_reward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PointFinished point))
        {
            Damage?.Invoke(-_damage);
            Die();
        }
    }

    private void Die()
    {
        Instantiate(_particleSystem, transform.position, Quaternion.identity);
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}
