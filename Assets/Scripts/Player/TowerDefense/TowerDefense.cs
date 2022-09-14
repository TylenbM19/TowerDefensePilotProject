using UnityEngine;
using UnityEngine.Events;

public class TowerDefense : PoolObject
{
    [SerializeField] private Transform _rotateHead;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePointPosition;
    [SerializeField] private float _rangeAttack;
    [SerializeField] private int _price;

    public int price { get; private set; }

    private const string enemyTarget = "Enemy";
    private GameObject _target;

    public static event UnityAction<int> OnDamage;

    private void Awake()
    {
        Initialize(_bullet);
    }

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    private void Update()
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

    private void Shoot(GameObject bullet)
    {
        bullet.SetActive(true);
        bullet.transform.position = _firePointPosition.position;
        bullet.transform.rotation = _rotateHead.transform.rotation;
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

            if (TryGetComponent(out GameObject bullet))
            {
                Shoot(bullet);
            }
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
