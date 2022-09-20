using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Coroutine _coroutine;
    private float _speedChangeSlider = 1f;
    private float _changeValue = 1f;
    private GameManager _playerWindow;

    private void OnEnable()
    {
        if (_playerWindow != null)
            _playerWindow.HealthChanged += OnValueChanged;
    }

    private void Start()
    {
        _playerWindow = Service.Instance.Get<GameManager>();
        OnEnable();
    }

    private void OnDisable()
    {
        _playerWindow.HealthChanged -= OnValueChanged;
    }

    private void OnValueChanged(int value, int maxValue)
    {
        _changeValue = (float)value / maxValue;
        CallCoroutine();
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
            _slider.value = Mathf.MoveTowards(_slider.value, _changeValue, _speedChangeSlider * Time.deltaTime);
            yield return fixedUpdateAwaiter;
        }
    }
}