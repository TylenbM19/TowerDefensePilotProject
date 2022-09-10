using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    [SerializeField] private float _speed;

    private Transform _point;
    private int _currentPosition;

    private void Start()
    {
        _point = WayPoints.Points[0];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);

        if(transform.position == _point.position)
        {
            if (_currentPosition >= WayPoints.Points.Length - 1)
            {
                _currentPosition = 0;
            }

            _currentPosition++;
            _point = WayPoints.Points[_currentPosition];
        }
    }
}
