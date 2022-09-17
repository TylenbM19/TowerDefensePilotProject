using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;

    public static event UnityAction<int> OnReward;
    public static event UnityAction<int> OnDamage;

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
            OnReward?.Invoke(_reward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PointFinished point))
        {
            Die();
            OnDamage?.Invoke(-_damage);
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
