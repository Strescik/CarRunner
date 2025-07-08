using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayManager : MonoBehaviour
{
    public static WayManager instance;

    [SerializeField] private GameObject _wayPrefab;
    [SerializeField] private List<GameObject> _enemysPrefab;

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

        if (_wayCount > 3)
            CreatEnemy();

        return pos;
    }
    private void CreatEnemy()
    {
        var rnd = Random.Range(0, 10);
        if (rnd < 3)
        {
            var rnd2 = Random.Range((_wayCount - 1) * distance, _wayCount * distance);

            switch (rnd % 3)
            {
                case 0:
                    Instantiate(_enemysPrefab[Random.Range(0, _enemysPrefab.Count)], new Vector3(0, 0, rnd2), Quaternion.identity);

                    break;
                case 1:
                    Instantiate(_enemysPrefab[Random.Range(0, _enemysPrefab.Count)], new Vector3(5, 0, rnd2), Quaternion.identity);

                    break;
                case 2:
                    Instantiate(_enemysPrefab[Random.Range(0, _enemysPrefab.Count)], new Vector3(-5, 0, rnd2), Quaternion.identity);

                    break;
                default:
                    break;

            }
        }
    }
}
