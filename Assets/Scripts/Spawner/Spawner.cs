using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemy;
    [SerializeField] private Transform _spawn;
    [SerializeField] private WayPoints _wayPoints;

    private float _countdonw = 6f;

    private int _countEnemy = 0;

    private void Update()
    {
            StartCoroutine(SpawnWave());

        if(_countEnemy != 5)
                StopCoroutine(SpawnWave());
        
    }

    IEnumerator SpawnWave()
    {
        SpawnEnemy();
        _countEnemy++;
        yield return new WaitForSeconds(3f);
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(_enemy[0], _spawn.position, _spawn.rotation);

        if (enemy.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
            enemyMovement.SetPath(_wayPoints.Points);
    }
}
