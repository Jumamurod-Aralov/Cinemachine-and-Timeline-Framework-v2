using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float _rotSpeed = 60f;
    [SerializeField] private float _rollSpeed = 90f;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 20f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _maxSpeed = 50f;

    private float _currentSpeed = 0f;
    private float _vertical;
    private float _horizontal;

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    // -------- ROTATION --------
    private void HandleRotation()
    {
        _vertical = Input.GetAxis("Vertical");     // Pitch
        _horizontal = Input.GetAxis("Horizontal"); // Yaw

        float yaw = _horizontal * _rotSpeed * Time.deltaTime;
        float pitch = -_vertical * _rotSpeed * Time.deltaTime;

        // Roll with Q and E
        float roll = 0f;
        if (Input.GetKey(KeyCode.Q))
            roll = _rollSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E))
            roll = -_rollSpeed * Time.deltaTime;

        transform.Rotate(pitch, yaw, roll, Space.Self);
    }

    // -------- MOVEMENT --------
    private void HandleMovement()
    {
        // Accelerate
        if (Input.GetKey(KeyCode.T))
        {
            _currentSpeed += _acceleration * Time.deltaTime;
        }

        // Decelerate
        if (Input.GetKey(KeyCode.G))
        {
            _currentSpeed -= _acceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, _maxSpeed);

        transform.position += transform.forward * _currentSpeed * _moveSpeed * Time.deltaTime;
    }
}