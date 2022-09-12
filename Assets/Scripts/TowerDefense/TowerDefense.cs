using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerDefense : MonoBehaviour
{
    [SerializeField] private float _rangeAttack;
    [SerializeField] private Transform _rotateHead;
    [SerializeField] private Bullet _bullet;
    //[SerializeField] private float _fireRate;
    [SerializeField] private Transform _firePointPosition;

    private const string enemyTarget = "Enemy";
    private GameObject _target;

    public static event UnityAction<int> OnDamage;

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

    private void Shoot()
    {
        Bullet bulletGO = Instantiate(_bullet, _firePointPosition.position, _firePointPosition.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(_target);
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
