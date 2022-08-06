using UnityEngine;

public class AsteroidController : BaseController
{
    private new AsteroidModel _model;
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        var pointsScale = 1.5f;
        if (_model.Size == AsteroidSize.Big)
        {
            pointsScale = 1;
        }
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Laser))
        {
            EventManager.OnDestroyEnemy?.Invoke(this, _model.Data.Points * pointsScale);
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
                EventManager.OnDestroyEnemy?.Invoke(this, _model.Data.Points * pointsScale);
                ObjectPool.ReturnToPool(poolable);
            }
        }
    }

    public override void Update(float timeStep)
    {
        FlyForward(timeStep, _model.SpeedScale);
    }

    private void Disassemble()
    {
        var parentDirection = _model.Base.MovementVector;
        var anglePerChild = Utils.Constants.AsteroidChildsRotationRange / (Utils.Constants.AsteroidChildCount - 1);
        var startDirection = Quaternion.Euler(0, 0, -Utils.Constants.AsteroidChildsRotationRange / 2) * parentDirection;
        for (var i = 0; i < Utils.Constants.AsteroidChildCount; i++)
        {
            var childDirection = Quaternion.Euler(0, 0, anglePerChild * i) * startDirection;
            ApplicationController.Instance.AsteroidSpawner.Spawn(AsteroidSize.Small, _model.Base.Position, childDirection);
        }
    }
}
