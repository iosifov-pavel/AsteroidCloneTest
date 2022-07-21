using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static class Constants
    {
        public const int PlayerMaxLazers = 10;

        public const float ShipMaxSpeed = 5f;
        public const float ShipInertiaRate = -0.008f;
        public const float ShipAccelerationRate = 0.016f;
        public const float ShipRotateSensibility = 0.008f;
        public const float RotationSpeed = 250f;

        public const float BulletDelay = 0.4f;
        public const float BulletSpeed = 6.5f;

        public const float AsteroidBigSize = 0.3f;
        public const float AsteroidSmallSize = 0.2f;
        public const float AsteroidSmallSpeedScale = 1.25f;
        public const float AsteroidChildsRotationRange = 45f;
        public const int AsteroidChildCount = 2;

        public const float LaserMaxWidth = 0.1f;
        public const float LaserMaxLength = 4f;
        public const float LaserAnimationTime = 0.5f;
    }


    public static Vector2 CalculateDirectionToPlayer(Vector2 position)
    {
        var playerPosition = ApplicationController.Instance.Player.Model.Base.Position;
        return (playerPosition - position).normalized;
    }


    public static bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }
}
