using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private int _damage;

    private WaitForFixedUpdate _waitForFixed;
    private Vector3 _target;
    private Coroutine _moveJob;

    private void Awake()
    {
        _waitForFixed = new WaitForFixedUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(-_damage);
            gameObject.SetActive(false);
            StopMove();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Wall>(out Wall wall))
        {
            gameObject.SetActive(false);
            StopMove();
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void Attack(Vector3 target)
    {
        _target = target;
        StartMove();
    }

    private void StartMove()
    {
        StopMove();
        _moveJob =  StartCoroutine(MoveCoroutine());
    }

    private void StopMove()
    {
        if (_moveJob != null)
            StopCoroutine(_moveJob);
    }

    private IEnumerator MoveCoroutine()
    {
        while (_target != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position,_target,_speed);
            yield return _waitForFixed;
        }
    }
}

