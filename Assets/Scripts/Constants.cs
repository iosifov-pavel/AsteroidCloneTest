using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const int PlayerBullets = 50;
    public const int PlayerLazers = 10;

    public const float ShipMaxSpeed = 5f;
    public const float ShipInertiaRate = -0.04f;
    public const float ShipAccelerationRate = 0.015f;
    public const float RotationSpeed = 250f;

    public const float BulletDelay = 0.4f;
    public const float BulletSpeed = 6.5f;

    public const float AsteroidSpeedRange = 2f;

    public const float AlienUpdatePlayerPositionTime = 0.75f;
    public const float AlienRotateTime = 0.5f;
    public const float AlienRotateTimeRandomModifier = 0.2f;
}
