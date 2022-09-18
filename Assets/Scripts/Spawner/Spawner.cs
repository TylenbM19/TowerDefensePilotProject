using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemy;
    [SerializeField] private Transform _spawn;
    [SerializeField] private WayPoints _wayPoints;
    [SerializeField] private AudioSource _spawnSound;
    [SerializeField] private ParticleSystem _spawnEffect;

    private int _maxIndex = 10;
    private int _currentIndexEnemy = 0;
    private int _currentIndex = 0;
    private float _waitForSeconds = 2.5f;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private void FixedUpdate()
    {
        if(_enemy[_currentIndexEnemy] == null)
            StopAllCoroutines();
    }

    IEnumerator SpawnWave()
    {
        while (_maxIndex  >= _currentIndex)
        {
            yield return new WaitForSeconds(_waitForSeconds);
            _spawnEffect.Play();
            _spawnSound.Play();
            SpawnEnemy();

            if (_currentIndex == _maxIndex)
            {
                ++_currentIndexEnemy;
                _currentIndex = 0;
            }
            else
            ++_currentIndex;
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(_enemy[_currentIndexEnemy], _spawn.position, _spawn.rotation);

        if (enemy.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
            enemyMovement.SetPath(_wayPoints.Points);
    }
}
