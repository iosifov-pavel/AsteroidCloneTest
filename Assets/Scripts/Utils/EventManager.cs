using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public EventHandler<Vector2> OnRotationChanged;
    public EventHandler<int> OnLaserCountChange;
    public EventHandler<Vector2> OnPlayerPositionChange;
    public EventHandler<float> OnPlayerScoreChange;
    public EventHandler<float> OnDestroyEnemy;
    public EventHandler<float> OnPlayerSpeedChange;
    public EventHandler<float> OnPlayerLaserCooldownChange;
    public EventHandler OnPlayerDeath;
    public EventHandler<Action<Vector2>> OnPlayerPositionRequest;
    public EventHandler<IUpdateable> OnSpawnNewController;
    public EventHandler<Transform> OnBulletSpawn;
    public EventHandler<KeyValuePair<Vector2,Vector2>> OnAsteroidDisassemble;
    public EventHandler OnGameStart;
}
