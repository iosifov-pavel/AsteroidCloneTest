using UnityEngine;

public static class Utils
{
    public static class Constants
    {
        public const int PlayerMaxLazers = 10;
        public const float PlayerLaserCooldown = 5f;

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
        public const float AsteroidSmallPointScale = 1.5f;

        public const float LaserMaxWidth = 0.1f;
        public const float LaserMaxLength = 4f;
        public const float LaserAnimationTime = 0.3f;
    }


    public static bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) > 0;
    }
}
