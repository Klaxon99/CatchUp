using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] private float _targerOffset;
    [SerializeField] private GroundChacker _groundChacker;

    private Vector3 _gravity;

    private void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        Vector3 forward = _target.position - transform.position;
        forward.y = transform.forward.y;
        transform.forward = forward;
    }

    private void Move()
    {
        if (_groundChacker.TryGetGroundContact(out RaycastHit hitInfo))
        {
            Vector3 moveDirection = Vector3.ProjectOnPlane(transform.forward, hitInfo.normal).normalized;
            float dotProduct = Vector3.Dot(transform.forward, moveDirection);

            if (Mathf.Approximately(dotProduct, 1f))
            {
                _gravity = Physics.gravity;
            }
            else
            {
                _gravity = Vector3.down;
            }

            if (Vector3.Distance(transform.position, _target.position) <= _targerOffset)
            {
                moveDirection = Vector3.zero;
            }

            _rigidbody.velocity = moveDirection * _movementSpeed + _gravity;
        }
    }
}