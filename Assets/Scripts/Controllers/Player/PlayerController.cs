using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController<PlayerModel>, IPlayerController
{
    private ShipControl _input;
    private bool _isRotating;
    private bool _isMoving;
    private bool _isFiring;
    private float _rechargeTimer;
    private float _rotationDirection;

    private Transform _bulletInitialPosition;

    public void MovePlayer(bool engineIsOn, float timeStep)
    {
        CalculateMovement(engineIsOn);
        _model.Base.Position += _model.Base.MovementVector * timeStep * _model.Acceleration;
    }

    public void RotatePlayer(float rotationDirection, float timeStep)
    {
        var rotation = -rotationDirection * timeStep * Utils.Constants.RotationSpeed;
        _model.Forward = Quaternion.Euler(0, 0, rotation) * _model.Forward;
    }

    public void ShootBullet(Transform bulletInitialPosition)
    {
        ApplicationController.Instance.SpawnBullet(bulletInitialPosition);
    }

    private void CalculateMovement(bool engineIsOn)
    {
        _model.Acceleration += engineIsOn ? Utils.Constants.ShipAccelerationRate : Utils.Constants.ShipInertiaRate;
        _model.Acceleration = Mathf.Clamp(_model.Acceleration, 0, Utils.Constants.ShipMaxSpeed);
        if(!engineIsOn)
        {
            return;
        }
        _model.Base.MovementVector = Vector2.Lerp(_model.Base.MovementVector, _model.Forward, Utils.Constants.ShipRotateSensibility);
    }
    public void SetInput(Transform bulletInitialPosition)
    {
        _bulletInitialPosition = bulletInitialPosition;
        _input = new ShipControl();
        _input.Ship.Rotate.performed += (c) => CheckRotateButton(c);
        _input.Ship.MoveForward.performed += (c) => CheckMoveButton(c);
        _input.Ship.Fire.performed += (c) => CheckShootButtonBullet(c);
        _input.Ship.Laser.performed += (_) => ShootLaser();
        _input.Enable();
    }
    public void CheckRotateButton(InputAction.CallbackContext context)
    {
        _rotationDirection = context.ReadValue<float>();
        _isRotating = _rotationDirection != 0;
    }
    public void CheckMoveButton(InputAction.CallbackContext context)
    {
        _isMoving = context.action.IsPressed();
    }
    public void CheckShootButtonBullet(InputAction.CallbackContext context)
    {
        _isFiring = context.action.IsPressed();
        if (!_isFiring)
        {
            _rechargeTimer = 0;
        }
    }
    public void ShootLaser()
    {
        Debug.Log("piu");
    }

    public override void Update(float timeStep)
    {
        _rechargeTimer -= timeStep;
        if (_isRotating)
        {
            RotatePlayer(_rotationDirection, timeStep);
        }
        MovePlayer(_isMoving, timeStep);
        if (_isFiring)
        {
            if (_rechargeTimer <= 0)
            {
                ShootBullet(_bulletInitialPosition);
                _rechargeTimer = Utils.Constants.BulletDelay;
            }
        }
    }
}
