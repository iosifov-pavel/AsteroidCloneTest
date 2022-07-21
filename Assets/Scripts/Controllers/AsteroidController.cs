using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : BaseController<AsteroidModel>
{
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Bullet))
        {
            if(_model.Size == AsteroidSize.Big)
            {
                Disassemble();
            }
            ObjectPool.ReturnToPool(poolable);
        }
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Laser))
        {
            ObjectPool.ReturnToPool(poolable);
        }
    }

    protected override void CheckExitCollision(Collider2D collision, IPoolable poolable)
    {
        if(Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Screen))
        {
            ObjectPool.ReturnToPool(poolable);
        }
    }

    public override void Update(float timeStep)
    {
        FlyForward(timeStep, _model.SpeedScale);
    }

    private void Disassemble()
    {
        var parentDirection = _model.Base.MovementVector;
        var anglePerChild = Utils.Constants.AsteroidChildsRotationRange / ( Utils.Constants.AsteroidChildCount - 1);
        var startDirection = Quaternion.Euler(0, 0, -Utils.Constants.AsteroidChildsRotationRange / 2) * parentDirection;
        for( var i = 0; i < Utils.Constants.AsteroidChildCount; i++)
        {
            var childDirection = Quaternion.Euler(0, 0, anglePerChild * i) * startDirection;
            ApplicationController.Instance.AsteroidSpawner.Spawn(AsteroidSize.Small, _model.Base.Position, childDirection);
        }
    }
}
