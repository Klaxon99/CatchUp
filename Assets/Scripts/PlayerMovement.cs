using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _verticalMinAngle;
    [SerializeField] private float _verticalMaxAngle;
    [SerializeField] private float _turnSensivity;
    [SerializeField] private GroundChacker _groundChacker;

    private float _verticalCameraAngle;

    private void Awake()
    {
        _verticalCameraAngle = _camera.localEulerAngles.x;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_camera.transform.right, Vector3.up).normalized;
        forward *= Input.GetAxis("Vertical");
        right *= Input.GetAxis("Horizontal");

        Vector3 direction = forward + right;

        if (_groundChacker.TryGetGroundContact(out RaycastHit hitInfo))
        {
            direction = Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;
        }

        _characterController.Move(direction * _movementSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        _verticalCameraAngle -= Input.GetAxis("Mouse Y") * _turnSensivity;
        _verticalCameraAngle = Mathf.Clamp(_verticalCameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _camera.localEulerAngles = Vector3.right * _verticalCameraAngle;

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * _turnSensivity);
    }
}