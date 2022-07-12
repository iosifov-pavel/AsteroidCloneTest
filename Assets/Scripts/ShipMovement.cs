using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    private bool _isRotating;
    private bool _isMoving;

    private float _rotationDirection;
    private float _currentSpeed;

    public void RotateShip(InputAction.CallbackContext context)
    {
        _rotationDirection = context.ReadValue<float>();
        _isRotating = _rotationDirection != 0;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _isMoving = context.action.IsPressed();
    }

    private void Update()
    {
        if (_isRotating)
        {
            var rotation = -_rotationDirection * Time.deltaTime * Constants.RotationSpeed;
            transform.Rotate(new Vector3(0, 0, rotation));
        }
        CalculateSpeed();
        var move = Vector2.up * Time.deltaTime * _currentSpeed;
        transform.Translate(move);
    }

    private void CalculateSpeed()
    {
        var accelerationChange = _isMoving ? Constants.ShipAccelerationRate : Constants.ShipInertiaRate;
        _currentSpeed += accelerationChange;
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, Constants.ShipMaxSpeed);
    }


}
