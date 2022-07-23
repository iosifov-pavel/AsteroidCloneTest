using UnityEngine;

public interface IPlayerController
{
    public void RotatePlayer(float rotationDirection, float timeStep);

    public void ShootBullet(Transform bulletInitialPosition);

    public void MovePlayer(bool engineIsOn, float timeStep);
}
