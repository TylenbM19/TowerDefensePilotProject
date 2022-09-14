using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private int _currentHealth;
    [SerializeField] private PointFinished _pointFinished;

    public static event UnityAction<int> OnReward;
    public static event UnityAction<int> OnDamage;

    private void Start()
    {
        _currentHealth = _health;
    }

    private void OnEnable()
    {
        TowerDefense.OnDamage += TakeDamage;
    }

    private void OnDisable()
    {
        TowerDefense.OnDamage -= TakeDamage;      
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
        GameObject.Destroy(this.gameObject);
    }
}
