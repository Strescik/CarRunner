using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayManager : MonoBehaviour
{
    public static WayManager instance;

    [SerializeField] private GameObject _wayPrefab;
    private int _wayCount;
    private float distance = 45f;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance);
        instance = this;

        for (int i = 0; i < 5; i++)
        {
            Instantiate(_wayPrefab, NewPosition(), Quaternion.identity);
        }
    }

    internal Vector3 NewPosition()
    {
        if (_wayCount == 0)
        {
            _wayCount++;
            return Vector3.zero;
        }
        var pos = Vector3.forward * _wayCount * distance;
        _wayCount++;

        return pos;
    }
}
