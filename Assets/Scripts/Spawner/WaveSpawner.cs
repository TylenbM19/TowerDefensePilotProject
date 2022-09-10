using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemy;
    [SerializeField] private Transform _spawn;

    public static event UnityAction<int> OnWave;

    private float _countdonw = 6f;
    private int _waveIndex = 0;

    private void Update()
    {
        if (_countdonw <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdonw = 2f;
        }

        _countdonw -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        _waveIndex++;

        for (int i = 0; i < 1; i++)
        {
            OnWave?.Invoke(_waveIndex);
            SpawnEnemy();
            yield return new WaitForSeconds(3f);
        }
    }

    private void SpawnEnemy()
    {
        GameObject.Instantiate(_enemy[0], _spawn.position, _spawn.rotation);
    }
}
