using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipController : BaseController<AlienShipModel>
{
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Bullet))
        {
            ObjectPool.ReturnToPool(poolable);
            ChangePlayerScore(_model.Data.Points);
        }
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Laser))
        {
            ObjectPool.ReturnToPool(poolable);
            ChangePlayerScore(_model.Data.Points);
        }
    }

    protected override void CheckExitCollision(Collider2D collision, IPoolable poolable)
    {
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Screen))
        {
            ObjectPool.ReturnToPool(poolable);
        }
    }

    public override void Update(float timeStep)
    {
        _model.Base.MovementVector = Utils.CalculateDirectionToPlayer(_model.Base.Position);
        _model.Base.Position += _model.Base.MovementVector * timeStep * _model.Data.Speed;
    }
}
