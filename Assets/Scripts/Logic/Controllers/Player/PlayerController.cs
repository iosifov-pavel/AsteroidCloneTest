using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController, IPlayerController
{
    private ShipControl _input;
    private new PlayerModel _model;
    private bool _isRotating;
    private bool _isMoving;
    private bool _isFiring;
    private float _rechargeTimer;
    private float _laserCooldownTimer;
    private float _rotationDirection;
    private bool _isDead;

    private Transform _bulletInitialPosition;

    public override void Setup(BaseModel model)
    {
        _model = (PlayerModel)model;
    }

    public void MovePlayer(bool engineIsOn, float timeStep)
    {
        CalculateMovement(engineIsOn);
        _model.Base.Position += _model.Base.MovementVector * timeStep * _model.Acceleration;
        EventManager.OnPlayerPositionChange?.Invoke(this, _model.Base.Position);
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
        if (!engineIsOn)
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
        _input.Ship.Laser.performed += (_) => CheckLaserButton();
        _input.Enable();

        EventManager.OnDestroyEnemy += UpdatePlayerScore;
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
    public void CheckLaserButton()
    {
        if (_isDead)
        {
            return;
        }
        if (!_model.CanShootLazer)
        {
            return;
        }
        ShootLaser();
    }

    private void ShootLaser()
    {
        _model.ShootLaser();
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
        if (_model.IsLaserMaxCapacity)
        {
            return;
        }
        _laserCooldownTimer += timeStep;
        EventManager.OnPlayerLaserCooldownChange?.Invoke(this, Utils.Constants.PlayerLaserCooldown - _laserCooldownTimer);
        if (_laserCooldownTimer >= Utils.Constants.PlayerLaserCooldown)
        {
            _model.RestoreLaser();
            _laserCooldownTimer = 0;
        }
    }
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if(collision.gameObject.TryGetComponent<BaseView>( out var view))
        {
            if(view.Model.Data.Type == ObjectType.Asteroid || view.Model.Data.Type == ObjectType.AlienShip)
            {
                EventManager.OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    protected override void CheckExitCollision(Collider2D collision, IPoolable poolable)
    {
        if (!Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Screen))
        {
            return;
        }
        var Xdiff = ApplicationController.Instance.LevelBounds.bounds.extents.x - Mathf.Abs(_model.Base.Position.x);
        var Ydiff = ApplicationController.Instance.LevelBounds.bounds.extents.y - Mathf.Abs(_model.Base.Position.y);
        if (Xdiff >= Ydiff)
        {
            _model.Base.Position = new Vector2(_model.Base.Position.x, -_model.Base.Position.y);
        }
        else
        {
            _model.Base.Position = new Vector2(-_model.Base.Position.x, _model.Base.Position.y);
        }
    }

    private void UpdatePlayerScore(object sender, float scoreChange)
    {
        _model.Score += scoreChange;
    }
}
