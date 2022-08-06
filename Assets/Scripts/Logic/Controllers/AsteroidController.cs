using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : BaseController
{
    private new AsteroidModel _model;
    public const float AsteroidChildsRotationRange = 45f;
    public const int AsteroidChildCount = 2;
    public override void Setup(BaseModel model)
    {
        _model = (AsteroidModel)model;
    }
    public override void FlyForward(float deltaTime)
    {
        _model.Base.Position += _model.Base.MovementVector * deltaTime * _model.Data.Speed * _model.SpeedScale;
    }
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        var pointsScale = 1.5f;
        if (_model.Size == AsteroidSize.Big)
        {
            pointsScale = 1;
        }
        if (IsInLayerMask(collision.gameObject, _levelData.Laser))
        {
            _eventManager.OnDestroyEnemy?.Invoke(this, _model.Data.Points * pointsScale);
            ObjectPool.ReturnToPool(poolable);
        }
        else if (collision.gameObject.TryGetComponent<BaseView>(out var view))
        {
            if (view.Model.Data.Type == ObjectType.Bullet)
            {
                if (_model.Size == AsteroidSize.Big)
                {
                    Disassemble();
                }
                _eventManager.OnDestroyEnemy?.Invoke(this, _model.Data.Points * pointsScale);
                ObjectPool.ReturnToPool(poolable);
            }
        }
    }

    public override void Update(float timeStep)
    {
        FlyForward(timeStep);
    }

    private void Disassemble()
    {
        var parentDirection = _model.Base.MovementVector;
        var anglePerChild = AsteroidChildsRotationRange / (AsteroidChildCount - 1);
        var startDirection = Quaternion.Euler(0, 0, -AsteroidChildsRotationRange / 2) * parentDirection;
        for (var i = 0; i < AsteroidChildCount; i++)
        {
            var childDirection = Quaternion.Euler(0, 0, anglePerChild * i) * startDirection;
            _eventManager.OnAsteroidDisassemble?.Invoke(this, new KeyValuePair<Vector2, Vector2>(_model.Base.Position, childDirection));
        }
    }
}
