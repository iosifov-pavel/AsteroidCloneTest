using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : BaseController<BulletModel>
{
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Enemy))
        {
            ObjectPool.ReturnToPool(poolable);
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
        FlyForward(timeStep);
    }
}
