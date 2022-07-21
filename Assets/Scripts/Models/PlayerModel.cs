using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : BaseModel
{
    private int _laserCount;
    private float _acceleration;
    private Vector2 _forward;

    public PlayerModel(ObjectData data, Vector2 position) : base(data, position)
    {
        _laserCount = Utils.Constants.PlayerMaxLazers;
        _forward = Vector2.up;
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
        set => _acceleration = value;
    }

    public bool CanShootLazer => _laserCount > 0;

    public void ShootLaser()
    {
        _laserCount--;
        OnShootLaser?.Invoke();
    }

    public UnityEvent OnShootLaser = new UnityEvent();
}
