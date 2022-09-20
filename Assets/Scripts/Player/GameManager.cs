using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
struct TowerData
{
    public Tower Tower;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<TowerData> _towers = new List<TowerData>();
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _energy = 20;

    public event UnityAction<Tower> Tower;
    public event UnityAction<int> CurrentBalanse;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction OnDie;

    private const int _minHealth = 0;
    private  int _currentHealth = 0;

    public int CurrentEnergy { get; private set; }

    private void Awake()
    {
        Service.Instance.Register(this);
        CurrentEnergy = _energy;
    }

    private void OnEnable()
    {
        Enemy.ConsumeReward += ApplyReward;
        Enemy.Damage += ChangeHealth;
    }

    private void OnDisable()
    {
        Enemy.Damage -= ApplyReward;
        Enemy.Damage -= ChangeHealth;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        CurrentEnergy = _energy;
    }

    public void Bye(int price)
    {
        CurrentEnergy -= price;
        CurrentBalanse?.Invoke(CurrentEnergy);
    }

    private void ChangeHealth(int value)
    {
        int tempCurrentHealth = Mathf.Clamp(_currentHealth + value, _minHealth, _maxHealth);

        if(CheckHealth(tempCurrentHealth))
            OnDie?.Invoke();

        if (tempCurrentHealth != _currentHealth && _currentHealth >= _minHealth)
        {
            _currentHealth = tempCurrentHealth;
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    private bool CheckHealth(int value)
    {              
        return value == _minHealth;
    }

    private void ApplyReward(int reward)
    {
        CurrentEnergy += reward;
        CurrentBalanse?.Invoke(CurrentEnergy);
    }
}
