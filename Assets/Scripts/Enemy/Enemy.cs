using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private int _currentHealth;
    [SerializeField] private PointFinished _pointFinished;

    public static event UnityAction<int> OnDie;

    //private int _currentHealth;

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
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PointFinished point))
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject.Destroy(this.gameObject);
        //OnDie?.Invoke(_reward);
    }
}
