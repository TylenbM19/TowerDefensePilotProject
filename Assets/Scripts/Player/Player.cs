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
    [SerializeField] private int _currentHealth = 0;

    public event UnityAction<TowerDefense> OnTower;
    public event UnityAction<int> OnReward;
    public event UnityAction<int, int> OnHealthChanged;

    private TowerDefense _currentTower = null;
    private int _minHealth = 0;

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

    private void ChangeHealth(int value)
    {
        int tempCurrentHealth = Mathf.Clamp(_currentHealth + value, _minHealth, _maxHealth);

        if (tempCurrentHealth != _currentHealth)
        {
            _currentHealth = tempCurrentHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    private void ApplyReward(int reward)
    {
        CurrentEnergy += reward;
        OnReward?.Invoke(CurrentEnergy);
    }

    private void SearchObjectArray(string nameTower)
    {
        foreach (var tower in _towers)
        {
            if (tower.Name == nameTower)
            {             
                _currentTower = tower.Tower;
                bool _check = PriceCheck();
                PassObject(_check);
                break;
            }
        }
    }

    private bool PriceCheck()
    {
        return _currentTower.Price <= CurrentEnergy;
    }

    private void PassObject(bool check)
    {
        if (_currentTower != null && check)
        {
            OnTower?.Invoke(_currentTower);
            _currentTower = null;
        }
        else
        {
            OnTower?.Invoke(null);
        }
    }
}
