using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 70f;
    [SerializeField] private int _damage;

    private GameObject _target;

    private void Update()
    {    
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _target.transform)
        {
            Destroy(gameObject);
            _target.GetComponent<Enemy>()?.TakeDamage(-_damage);
        }
    }

    public void Seek(GameObject target)
    {
        _target = target;
    }

    private void Move()
    {
        if (_target == null)
            return;

        Vector3 direction = _target.transform.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime, Space.World);
    }
}

