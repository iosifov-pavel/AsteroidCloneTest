using System;
using UnityEngine;

public static class EventManager
{
    public static EventHandler<Vector2> OnRotationChanged;
    public static EventHandler<int> OnLaserCountChange;
    public static EventHandler<Vector2> OnPlayerPositionChange;
    public static EventHandler<float> OnPlayerScoreChange;
    public static EventHandler<float> OnDestroyEnemy;
    public static EventHandler<float> OnPlayerSpeedChange;
    public static EventHandler<float> OnPlayerLaserCooldownChange;
    public static EventHandler OnPlayerDeath;
}
