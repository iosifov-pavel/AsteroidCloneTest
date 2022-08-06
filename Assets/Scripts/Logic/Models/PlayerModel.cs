using System;
using UnityEngine;

public class PlayerModel : BaseModel
{
    public const int LaserCapacity = 10;
    public const float LaserAnimationTime = 0.3f;

    private int _laserCount;
    private float _acceleration;
    private Vector2 _forward;
    private float _score;

    public Action OnShootLaser;
    public Action<Vector2> OnPlayerRotationChange;
    public Action<int> OnLaserCountChange;

    public PlayerModel(ObjectData data, Vector2 position) : base(data, position)
    {
        LaserCount = LaserCapacity;
        _forward = Vector2.up;
    }
    public Vector2 Forward
    {
        get => _forward;
        set
        {
            _forward = value;
            OnPlayerRotationChange?.Invoke(_forward);
        }
    }
    public float Acceleration
    {
        get => _acceleration;
        set
        {
            _acceleration = value;
        }
    }
    public bool CanShootLazer => LaserCount > 0;
    public bool IsLaserMaxCapacity => _laserCount == LaserCapacity;
    public float Score
    {
        get => _score;
        set
        {
            _score = value;
        }
    }
    private int LaserCount
    {
        get => _laserCount;
        set
        {
            _laserCount = value;
            OnLaserCountChange?.Invoke(_laserCount);
        }
    }
    public void ShootLaser()
    {
        LaserCount--;
        OnShootLaser?.Invoke();
    }
    public void RestoreLaser()
    {
        LaserCount++;
    }
}
