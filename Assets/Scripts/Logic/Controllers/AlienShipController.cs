using UnityEngine;

public class AlienShipController : BaseController
{
    private new AlienShipModel _model;
    public override void Setup(BaseModel model)
    {
        _model = (AlienShipModel)model;
    }
    protected override void CheckEnterCollision(Collider2D collision, IPoolable poolable)
    {
        if (IsInLayerMask(collision.gameObject, _levelData.Laser))
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
        _eventManager.OnPlayerPositionRequest?.Invoke(this, (v) => PlayerPositionCallback(v,timeStep));
    }

    private void PlayerPositionCallback(Vector2 playerPosition, float timeStep)
    {
        _model.Base.MovementVector = (playerPosition - _model.Base.Position).normalized;
        _model.Base.Position += _model.Base.MovementVector * timeStep * _model.Data.Speed;
    }
}
