using UnityEngine;

public abstract class BaseController : IFlyForward, IUpdateable
{
    protected BaseModel _model;
    protected EventManager _eventManager;
    protected LevelData _levelData;
    public virtual BaseModel Model { get => _model; }

    public virtual void FlyForward(float deltaTime)
    {
        _model.Base.Position += _model.Base.MovementVector * deltaTime * _model.Data.Speed;
    }

    public void SetUtils(EventManager eventManager, LevelData levelData)
    {
        _eventManager = eventManager;
        _levelData = levelData;
    }

    public virtual void Setup(BaseModel model)
    {
        _model = model;
    }

    public abstract void Update(float timeStep);

    public void ProceedCollision(Collider2D collision, IPoolable poolable, bool isEnter)
    {
        if (isEnter)
        {
            CheckEnterCollision(collision, poolable);
        }
        else
        {
            CheckExitCollision(collision, poolable);
        }
    }

    protected abstract void CheckEnterCollision(Collider2D collision, IPoolable poolable);

    protected virtual void CheckExitCollision(Collider2D collision, IPoolable poolable)
    {
        if(!IsInLayerMask(collision.gameObject, _levelData.Mask))
        {
            return;
        }
        ObjectPool.ReturnToPool(poolable);
    }


    protected void ChangePlayerScore(float points)
    {
        _eventManager.OnDestroyEnemy?.Invoke(this, points);
    }

    protected bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) > 0;
    }
}
