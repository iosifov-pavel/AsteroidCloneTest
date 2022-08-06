using UnityEngine;

public class BulletController : BaseController
{
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if (collision.gameObject.TryGetComponent<BaseView>(out var view))
        {
            if (view.Model.Data.Type == ObjectType.Asteroid || view.Model.Data.Type == ObjectType.AlienShip)
            {
                ObjectPool.ReturnToPool(poolable);
            }
        }
    }

    public override void Update(float timeStep)
    {
        FlyForward(timeStep);
    }
}
