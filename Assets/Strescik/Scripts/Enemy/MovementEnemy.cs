using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }
}
