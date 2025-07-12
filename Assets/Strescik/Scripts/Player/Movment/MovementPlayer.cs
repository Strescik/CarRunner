using DG.Tweening;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed = .2f;

    private bool _isMoving = true;
    [SerializeField] private bool _isTurning = false;

    private void FixedUpdate()
    {
        if (!_isMoving)
            return;
        gameManager.SetPoint(transform.position.z);
    }
    private void Update()
    {
        if (!_isMoving)
            return;
        DefultMove();
    }

    private void DefultMove()
    {
        gameObject.transform.Translate(Vector3.forward * _speed * gameManager.lvSpeed * Time.deltaTime);
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
            //oyun bitti
            //_isMoving = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gold"))
        {
            GameManager.instance.AddGold();
            other.gameObject.SetActive(false);
        }
    }
}
