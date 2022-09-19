using UnityEngine;

public class TowerDefense : MonoBehaviour
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
    private GameObject _target;

    private void Awake()
    {
        _bulletPool = new Pool<Bullet>(_bulletPrefab);
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        RotateHead();
    }

    private void RotateHead()
    {
        Vector3 direction = _target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = lookRotation.eulerAngles;
        _rotateHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GetBullet();
        _shootSound.Play();
    }

    private void GetBullet()
    {
        Bullet bullet = _bulletPool.Get();
        bullet.SetPosition(_firePointPosition.position);
        bullet.SetRotation(_rotateHead.rotation);
        bullet.gameObject.SetActive(true);
        bullet.Attack(_target.transform.position);
    }

    private void UpdateTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(enemyTarget);
        float shortesDistance = Mathf.Infinity;
        GameObject currentTarget = null;

        foreach (GameObject enemy in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                currentTarget = enemy;
            }
        }

        if (currentTarget != null && shortesDistance <= _rangeAttack)
        {
            _target = currentTarget;

            Shoot();
        }
        else
            _target = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangeAttack);
    }
}
