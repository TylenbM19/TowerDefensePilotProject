using UnityEngine;

public class WayPoints : MonoBehaviour
{
    [SerializeField] private Transform _path;

    public static Transform[] Points;

    private void Awake()
    {
        Points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            Points[i] = _path.GetChild(i);
        }
    }
}
