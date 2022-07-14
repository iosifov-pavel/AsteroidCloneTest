using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : BaseView<PlayerModel,PlayerController>
{
    [SerializeField]
    private Transform _bulletInitialPosition;

    private ShipControl _input;
    private bool _isRotating;
    private bool _isMoving;

    private bool _isFiring;
    private float _rechargeTimer;

    private float _rotationDirection;
    private float _currentSpeed;

    public override void Setup(PlayerModel model)
    {
        base.Setup(model);
        ApplicationController.Instance.Player = _controller;
        SetInput();
    }
    protected override void SetCallbacks()
    {
        EventManager.OnRotationChanged += RotatePlayer;
        _model.Base.OnPositionChange.AddListener(MovePlayer);
    }
    private void SetInput()
    {
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

    private void Update()
    {
        if (!ReadyToUse)
        {
            return;
        }
        _rechargeTimer -= Time.deltaTime;
        if (_isRotating)
        {
            _controller.RotatePlayer(_rotationDirection, Time.deltaTime);
        }
        _controller.MovePlayer(_isMoving, Time.deltaTime);
        if (_isFiring)
        {
            if (_rechargeTimer <= 0)
            {
                _controller.ShootBullet(_bulletInitialPosition);
                _rechargeTimer = Utils.Constants.BulletDelay;
            }
        }
    }

    private void RotatePlayer(object sender, Vector2 newForward)
    {
        transform.up = newForward;
    }
    private void MovePlayer(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

}
