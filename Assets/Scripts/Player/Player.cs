using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
struct TowerData
{
    public string Name;
    public TowerDefense Tower;
}

public class Player : MonoBehaviour
{
    [SerializeField] private List<TowerData> _towers = new List<TowerData>();
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _energy = 20;

    public event UnityAction<TowerDefense?> OnTower;
    public event UnityAction<int> OnReward;
    public event UnityAction<int, int> OnHealthChanged;
    public static event UnityAction OnDie;

    private TowerDefense _currentTower = null;
    private int _currentHealth = 0;
    private int _minHealth = 0;
    private string _currentName;

    public int CurrentEnergy { get; private set; }

    private void Awake()
    {
        Service.Instance.Register(this);
        CurrentEnergy = _energy;
    }

    private void OnEnable()
    {
        Enemy.OnReward += ApplyReward;
        Enemy.OnDamage += ChangeHealth;
        ClickButton.ButtonClick += SearchObjectArray;
    }

    private void OnDisable()
    {
        Enemy.OnDamage -= ApplyReward;
        Enemy.OnDamage -= ChangeHealth;
        ClickButton.ButtonClick -= SearchObjectArray;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        CurrentEnergy = _energy;
    }

    public void Bye(int price)
    {
        CurrentEnergy -= price;
        OnReward?.Invoke(CurrentEnergy);
        _currentTower = null;
    }

    public void DisableBuild()
    {
        foreach (var tower in _towers)
        {
            if (tower.Name == _currentName)
            {
                _currentTower = tower.Tower;

                PassObject(null);
            }
        }
    }

    private void ChangeHealth(int value)
    {
        int tempCurrentHealth = Mathf.Clamp(_currentHealth + value, _minHealth, _maxHealth);

        HealthCheck(tempCurrentHealth);

        if (tempCurrentHealth != _currentHealth && _currentHealth >= _minHealth)
        {
            _currentHealth = tempCurrentHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    private void HealthCheck(int value)
    {
        if(value == _minHealth)
        {
            OnDie?.Invoke();
            Time.timeScale = 0;
        }
    }

    private void ApplyReward(int reward)
    {
        CurrentEnergy += reward;
        OnReward?.Invoke(CurrentEnergy);
    }

    private void SearchObjectArray(string nameTower)
    {
        _currentName = nameTower;

        foreach (var tower in _towers)
        {
            if (tower.Name == nameTower)
            {
                _currentTower = tower.Tower;

                if (PriceCheck(tower.Tower))
                    PassObject(tower.Tower);
                break;
            }
        }
    }

    private bool PriceCheck(TowerDefense tower)
    {
        return tower.Price <= CurrentEnergy;
    }

    private void PassObject(TowerDefense? tower)
    {
        OnTower?.Invoke(tower);
    }
}
