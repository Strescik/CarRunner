using DG.Tweening;
using UnityEngine;

public class MovementWay : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.D) && gameObject.transform.position.x < 5)//5
        {
            _isTurning = true;

            sequence.Append(transform.DOMoveX(gameObject.transform.position.x + 5, _turnSpeed))
                .AppendCallback(() => _isTurning = false);
        }
        else if (Input.GetKeyDown(KeyCode.A) && gameObject.transform.position.x > -5)//-5
        {
            _isTurning = true;

            sequence.Append(transform.DOMoveX(gameObject.transform.position.x - 5, _turnSpeed))
                .AppendCallback(() => _isTurning = false);

        }
    }

}
