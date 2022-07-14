using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController<PlayerModel>, IPlayerController
{
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
}
