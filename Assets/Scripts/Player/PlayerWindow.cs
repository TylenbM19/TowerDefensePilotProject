using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text _countGold;
    [SerializeField] private Slider _slider;
    [SerializeField] private int _maxValue = 100;
    [SerializeField] private int _changeValue;

    private int _countWave = 0;
    private int _goldtWave = 0;
    private Coroutine _coroutine;
    private float _speedChangeSlider = 1f;

    private int _minValue = 0;
    private float _value = 1f;

    private void Awake()
    {
        _changeValue = _maxValue;
    }

    private void OnEnable()
    {
        Enemy.GetValueAfterDeath += ChangeGold;
        Enemy.GetValueAfterDeath += ChangeHealth;
    }

    private void OnDisable()
    {
        Enemy.GetValueAfterDeath -= ChangeGold;
        Enemy.GetValueAfterDeath -= ChangeHealth;
    }

    private void Start()
    {
        _slider.value = _value;
    }

    private void ChangeGold(int _value)
    {
        _goldtWave += _value;
        _countGold.text = (_goldtWave.ToString());
    }

    private void ChangeHealth(int value)
    {
        Debug.Log(value);

        _changeValue = Mathf.Clamp(_changeValue + value, _minValue, _maxValue);

        if (_maxValue != _changeValue)
        {
            _value = (float)_changeValue / _maxValue;

            CallCoroutine();
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
