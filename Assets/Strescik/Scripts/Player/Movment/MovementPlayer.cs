using DG.Tweening;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed = .2f;

    private bool _isMoving = true;
    [SerializeField] private bool _isTurning = false;

    private void Update()
    {
        DefultMove();
    }

    private void DefultMove()
    {
        if (!_isMoving)
            return;

        gameObject.transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        TurnMotion();
    }

    private void TurnMotion()
    {
        if (_isTurning)
            return;

        Sequence sequence = DOTween.Sequence();

        if (Input.GetKeyDown(KeyCode.D) && gameObject.transform.position.x < 5)
        {
            _isTurning = true;

            sequence.Append(transform.DOMoveX(gameObject.transform.position.x + 5, _turnSpeed))
                .AppendCallback(() => _isTurning = false);
        }
        else if (Input.GetKeyDown(KeyCode.A) && gameObject.transform.position.x > -5)
        {
            _isTurning = true;

            sequence.Append(transform.DOMoveX(gameObject.transform.position.x - 5, _turnSpeed))
                .AppendCallback(() => _isTurning = false);

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //_isMoving = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gold"))
        {
            Debug.Log("Gold al�nd�");
            other.gameObject.SetActive(false);
        }
    }
}
