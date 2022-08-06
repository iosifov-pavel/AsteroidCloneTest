using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController, IPlayerController
{
    public const float PlayerLaserCooldown = 5f;
    public const float BulletDelay = 0.4f;
    public const float ShipMaxSpeed = 5f;
    public const float ShipInertiaRate = -0.008f;
    public const float ShipAccelerationRate = 0.016f;
    public const float RotationSpeed = 250f;
    public const float ShipRotateSensibility = 0.008f;

    private ShipControl _input;
    private new PlayerModel _model;
    private bool _isRotating;
    private bool _isMoving;
    private bool _isFiring;
    private bool _isDead;
    private float _rechargeTimer;
    private float _laserCooldownTimer;
    private float _rotationDirection;

    private Transform _bulletInitialPosition;
    public new PlayerModel Model { get => _model; }

    public override void Setup(BaseModel model)
    {
        _isDead = false;
        _model = (PlayerModel)model;
        _model.Score = 0;
        _model.OnLaserCountChange += CheckLasers;
        _eventManager.OnPlayerScoreChange?.Invoke(this, _model.Score);
        _eventManager.OnPlayerPositionRequest += OnPlayerPositionRequest;
        _eventManager.OnPlayerDeath += OnPlayerDeath;
    }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        _isDead = true;
        _input.Disable();
        _input.Ship.Rotate.performed -= (c) => CheckRotateButton(c);
        _input.Ship.MoveForward.performed -= (c) => CheckMoveButton(c);
        _input.Ship.Fire.performed -= (c) => CheckShootButtonBullet(c);
        _input.Ship.Laser.performed -= (_) => CheckLaserButton();
        _eventManager.OnDestroyEnemy -= UpdatePlayerScore;
    }

    private void OnPlayerPositionRequest(object sender, Action<Vector2> callback)
    {
        callback?.Invoke(_model.Base.Position);
    }
    public void MovePlayer(bool engineIsOn, float timeStep)
    {
        CalculateMovement(engineIsOn);
        _model.Base.Position += _model.Base.MovementVector * timeStep * _model.Acceleration;
        _eventManager.OnPlayerPositionChange?.Invoke(this, _model.Base.Position);
    }
    public void RotatePlayer(float rotationDirection, float timeStep)
    {
        var rotation = -rotationDirection * timeStep * RotationSpeed;
        _model.Forward = Quaternion.Euler(0, 0, rotation) * _model.Forward;
        _eventManager.OnRotationChanged?.Invoke(this, _model.Forward);
    }
    public void ShootBullet(Transform bulletInitialPosition)
    {
        _eventManager.OnBulletSpawn?.Invoke(this, bulletInitialPosition);
    }
    private void CalculateMovement(bool engineIsOn)
    {
        _model.Acceleration += engineIsOn ? ShipAccelerationRate : ShipInertiaRate;
        _model.Acceleration = Mathf.Clamp(_model.Acceleration, 0, ShipMaxSpeed);
        _eventManager.OnPlayerSpeedChange?.Invoke(this, _model.Acceleration);
        if (!engineIsOn)
        {
            return;
        }
        _model.Base.MovementVector = Vector2.Lerp(_model.Base.MovementVector, _model.Forward, ShipRotateSensibility);
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

        _eventManager.OnDestroyEnemy += UpdatePlayerScore;
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
                _rechargeTimer = BulletDelay;
            }
        }
        if (_model.IsLaserMaxCapacity)
        {
            return;
        }
        _laserCooldownTimer += timeStep;
        _eventManager.OnPlayerLaserCooldownChange?.Invoke(this, PlayerLaserCooldown - _laserCooldownTimer);
        if (_laserCooldownTimer >= PlayerLaserCooldown)
        {
            _model.RestoreLaser();
            _laserCooldownTimer = 0;
        }
    }
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if (collision.gameObject.TryGetComponent<BaseView>(out var view))
        {
            if (view.Model.Data.Type == ObjectType.Asteroid || view.Model.Data.Type == ObjectType.AlienShip)
            {
                _eventManager.OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    protected override void CheckExitCollision(Collider2D collision, IPoolable poolable)
    {
        if (!IsInLayerMask(collision.gameObject, _levelData.Mask))
        {
            return;
        }
        var Xdiff = _levelData.Bounds.bounds.extents.x - Mathf.Abs(_model.Base.Position.x);
        var Ydiff = _levelData.Bounds.bounds.extents.y - Mathf.Abs(_model.Base.Position.y);
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
        _eventManager.OnPlayerScoreChange?.Invoke(this, _model.Score);
    }

    private void CheckLasers(int laserCount)
    {
        _eventManager.OnLaserCountChange?.Invoke(this, laserCount);
    }
}
