using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
struct TowerData
{
    public string Name;
    public TowerDefense Tower;
}

public class PlayerWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _countGold;
    [SerializeField] private Slider _slider;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _changeValue;
    [SerializeField] private List<TowerData> _towers = new List<TowerData>();

    public static event UnityAction<TowerDefense> OnTower;

    public int CurrentGold
    {
        get { return _currentGold; }
        set 
        {
            _currentGold = value;
            _countGold.text = (_currentGold.ToString());
        }
    }

    private TowerDefense _currentTower = null;
    private bool _check;
    private int _countWave = 0;
    private int _currentGold = 20;
    private Coroutine _coroutine;
    private float _speedChangeSlider = 1f;
    private int _minValue = 0;
    private float _value = 1f;

    private void Awake()
    {
        _changeValue = _maxHealth;       
    }

    private void OnEnable()
    {
        Enemy.GetValueAfterDeath += (int e) => CurrentGold += e;
        Enemy.GetValueAfterDeath += ChangeHealth;
        ClickButton.ButtonClick += SearchObjectArray;
    }

    private void OnDisable()
    {
        Enemy.GetValueAfterDeath -= (int e) => CurrentGold += e;
        Enemy.GetValueAfterDeath -= ChangeHealth;
        ClickButton.ButtonClick -= SearchObjectArray;
    }

    private void Start()
    {
        _slider.value = _value;
    }

    private void ChangeHealth(int value)
    {
        _changeValue = Mathf.Clamp(_changeValue + value, _minValue, _maxHealth);

        if (_maxHealth != _changeValue)
        {
            _value = (float)_changeValue / _maxHealth;
            CallCoroutine();
        }
    }

    private void SearchObjectArray(string nameTower)
    {
        foreach (var tower in _towers)
        {
            if (tower.Name == nameTower)
            {
                _currentTower = tower.Tower;
                _check = PriceCheck();
                PassObject(_check);
            }
        }
    }

    private bool PriceCheck()
    {
        return _currentTower.price <= _currentGold;
    }

    private void PassObject(bool check)
    {
        Debug.Log(check);

        if (_currentTower != null && check)
        {
            OnTower?.Invoke(_currentTower);                                        
            _currentTower = null;
        }
    }

    private void CallCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoroutineSlider());
    }

    private IEnumerator CoroutineSlider()
    {
        var fixedUpdateAwaiter = new WaitForFixedUpdate();

        while (_slider.value != _changeValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _value, _speedChangeSlider * Time.deltaTime);
            yield return fixedUpdateAwaiter;
        }
    }
}
