using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private int _currentPosition = 0;
    private Transform[] _points;
    private Transform _target;
    
    private void Start()
    {
        _target = _points[_currentPosition];
    }

    private void Update()
    {
        Move(_target);
        Rotation(_target);
        CheckFromPosition();
    }

    public void SetPath(Transform[] points)
    {
        _points = points;
    }

    private void CheckFromPosition()
    {
        if (transform.position == _target.position)
        {
            if (_currentPosition >= _points.Length - 1)
            {
                _currentPosition = 0;
            }

            _currentPosition++;
            _target = _points[_currentPosition];
        }
    }

    private void Move(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    }

    private void Rotation(Transform target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
