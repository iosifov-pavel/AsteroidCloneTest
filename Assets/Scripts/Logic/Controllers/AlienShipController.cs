using UnityEngine;

public class AlienShipController : BaseController
{
    private new AlienShipModel _model;
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if (Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Laser))
        {
            ObjectPool.ReturnToPool(poolable);
            ChangePlayerScore(_model.Data.Points);
        }
        else if (collision.gameObject.TryGetComponent<BaseView>(out var view))
        {
            if (view.Model.Data.Type == ObjectType.Bullet)
            {
                ObjectPool.ReturnToPool(poolable);
                ChangePlayerScore(_model.Data.Points);
            }
        }
    }

    public override void Update(float timeStep)
    {
        _model.Base.MovementVector = ApplicationController.Instance.CalculateDirectionToPlayer(_model.Base.Position);
        _model.Base.Position += _model.Base.MovementVector * timeStep * _model.Data.Speed;
    }
}
