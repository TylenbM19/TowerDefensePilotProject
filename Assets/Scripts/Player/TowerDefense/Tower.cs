using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _rotateHead;
    [SerializeField] private Transform _firePointPosition;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _rangeAttack;
    [SerializeField] private int _price;
    [SerializeField] private AudioSource _shootSound;

    public int Price { get => _price; private set { } }

    private Pool<Bullet> _bulletPool;
    private const string enemyTarget = "Enemy";
    private List<Enemy> _enemies = new List<Enemy>();
    private Enemy _target;
    private Coroutine _coroutine;
    private WaitForSeconds _waitForSecond;
    private float _shootDelay = 1f;

    private void Awake()
    {
        _bulletPool = new Pool<Bullet>(_bulletPrefab);
        _waitForSecond = new WaitForSeconds(_shootDelay);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(MoveCoroutine());
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        RotateHead();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _enemies.Add(enemy);
            enemy.Died += RemoveInspectedEnemy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.Died -= RemoveInspectedEnemy;
            RemoveInspectedEnemy(enemy);
        }
    }

    private void RemoveInspectedEnemy(Enemy enemy)
    {
        if (_target == enemy)
            _target = null;

        _enemies.Remove(enemy);
    }

    private void RotateHead()
    {
        Vector3 direction = _target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = lookRotation.eulerAngles;
        _rotateHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            if (_target != null)
                Shoot();
            else
                UpdateTarget();

            yield return _waitForSecond;
        }
    }

    private void Shoot()
    {
        SpawnBullet();
        _shootSound.Play();
    }

    private void SpawnBullet()
    {
        Bullet bullet = _bulletPool.Get();
        bullet.transform.position = _firePointPosition.position;
        bullet.transform.rotation = _rotateHead.rotation;
        bullet.gameObject.SetActive(true);
        bullet.Attack(_target.transform.position);
    }

    private void UpdateTarget()
    {
        float shortesDistance = Mathf.Infinity;
        Enemy currentTarget = null;

        foreach (Enemy enemy in _enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                currentTarget = enemy;
            }
        }

        if (currentTarget != null && shortesDistance <= _rangeAttack)
            _target = currentTarget;
        else
            _target = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeAttack);
    }
}
