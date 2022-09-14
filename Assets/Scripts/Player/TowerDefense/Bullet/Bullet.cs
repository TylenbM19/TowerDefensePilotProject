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
            gameObject.SetActive(false);
            _target.GetComponent<Enemy>()?.TakeDamage(-_damage);
        }
        gameObject.SetActive(false);
    }

    public void Seek(GameObject target)
    {
        _target = target;
    }

    private void Move()
    {
        if (_target == null)
            return;

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}

