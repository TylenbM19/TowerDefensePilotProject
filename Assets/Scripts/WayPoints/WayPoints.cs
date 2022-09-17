using UnityEngine;
using System.Linq;

public class WayPoints : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Transform[] _points;
    public Transform[] Points => _points.Where(p => p != null).ToArray();

    private void Awake()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }
}
