using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class WayManager : MonoBehaviour
{
    public static WayManager instance;

    [SerializeField] private GameObject _wayPrefab;
    [SerializeField] private Transform _parentOther;
    [SerializeField] private Transform _parentEnemy;
    [SerializeField] private List<GameObject> _staticEnemysPrefab;
    [SerializeField] private List<GameObject> _dynamicEnemysPrefab;
    [SerializeField] private byte _startWayCount;

    private int _wayCount;
    private float distance = 45f;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(instance);
        instance = this;

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
            CreatStaticEnemy();

        if (_wayCount > 10)
            if (_wayCount % 5 == 0 && GameManager.instance.lvSpeed <= 4)
                GameManager.instance.UpLvSpeed();

        return pos;
    }
    private void CreatStaticEnemy()
    {
        var rnd = Random.Range(0, 10);
        if (rnd < 3)
        {
            var rndZPos = Random.Range((_wayCount - 1) * distance, _wayCount * distance);

            switch (rnd % 3)
            {
                case 0:
                    Instantiate(_staticEnemysPrefab[Random.Range(0, _staticEnemysPrefab.Count)], new Vector3(0, 0, rndZPos), Quaternion.identity, _parentEnemy);

                    break;
                case 1:
                    Instantiate(_staticEnemysPrefab[Random.Range(0, _staticEnemysPrefab.Count)], new Vector3(5, 0, rndZPos), Quaternion.identity, _parentEnemy);

                    break;
                case 2:
                    Instantiate(_staticEnemysPrefab[Random.Range(0, _staticEnemysPrefab.Count)], new Vector3(-5, 0, rndZPos), Quaternion.identity, _parentEnemy);

                    break;
                default:
                    break;

            }
        }

        rnd = Random.Range(0, 10);

        if (rnd < 8 && rnd >= 0)
        {
            CreatDynamicEnemy(rnd);
        }
    }
    private void CreatDynamicEnemy(int value)
    {
        var rndZPos = Random.Range((_wayCount - 1) * distance, _wayCount * distance);
        switch (value % 3)
        {
            case 0:
                Instantiate(_dynamicEnemysPrefab[Random.Range(0, _dynamicEnemysPrefab.Count)], new Vector3(0, 0, rndZPos), Quaternion.identity, _parentEnemy);

                break;
            case 1:
                Instantiate(_dynamicEnemysPrefab[Random.Range(0, _dynamicEnemysPrefab.Count)], new Vector3(5, 0, rndZPos), Quaternion.identity, _parentEnemy);

                break;
            case 2:
                Instantiate(_dynamicEnemysPrefab[Random.Range(0, _dynamicEnemysPrefab.Count)], new Vector3(-5, 0, rndZPos), Quaternion.identity, _parentEnemy);

                break;
            default:
                break;

        }
    }

    public void StartGame()
    {
        var enemy = Instantiate(new GameObject("Enemys"), Vector3.zero, Quaternion.identity);
        var Other = Instantiate(new GameObject("Other"), Vector3.zero, Quaternion.identity);
        _parentEnemy = enemy.transform;
        _parentOther = Other.transform;

        for (int i = 0; i < _startWayCount; i++)
        {
            Instantiate(_wayPrefab, NewPosition(), Quaternion.identity, _parentOther);
        }
        GameManager.instance.player.WaitToMove();
    }
    public void RestartGame()
    {
        Destroy(_parentEnemy.gameObject);
        Destroy(_parentOther.gameObject);

        _wayCount = 0;

        StartGame();

    }


}
