using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalDead : MonoBehaviour
{
    [SerializeField] private float _timeLife;
    private void Start()
    {
        Destroy(gameObject, _timeLife);
    }
}
