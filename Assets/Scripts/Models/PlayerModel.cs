using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : BaseModel
{
    private int _laserCount;
    private float _acceleration;
    private Vector2 _forward;
    private float _score;

    public PlayerModel(ObjectData data, Vector2 position) : base(data, position)
    {
        LaserCount = Utils.Constants.PlayerMaxLazers;
        _forward = Vector2.up;
        Score = 0;
    }
    public Vector2 Forward
    {
        get => _forward;
        set
        {
            _forward = value;
            EventManager.OnRotationChanged?.Invoke(this, _forward);
        }
    }

    public float Acceleration
    {
        get => _acceleration;
        set
        {
            _acceleration = value;
            EventManager.OnPlayerSpeedChange?.Invoke(this, _acceleration);
        }
    }

    public bool CanShootLazer => LaserCount > 0;
    public bool IsLaserMaxCapacity => _laserCount == Utils.Constants.PlayerMaxLazers;
    private int LaserCount
    {
        get => _laserCount;
        set
        {
            _laserCount = value;
            EventManager.OnLaserCountChange?.Invoke(this, _laserCount);
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
    public UnityEvent OnShootLaser = new UnityEvent();


    public float Score
    {
        get => _score;
        set
        {
            _score = value;
            EventManager.OnPlayerScoreChange?.Invoke(this, _score);
        }
    }
}
