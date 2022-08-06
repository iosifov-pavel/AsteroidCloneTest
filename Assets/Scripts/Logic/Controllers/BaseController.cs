using UnityEngine;

public abstract class BaseController : IFlyForward
{
    protected BaseModel _model;
    public BaseModel Model { get => _model; set => _model = value; }

    public void FlyForward(float deltaTime, float speedScale = 1)
    {
        _model.Base.Position += _model.Base.MovementVector * deltaTime * _model.Data.Speed * speedScale;
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
        if(!Utils.IsInLayerMask(collision.gameObject, ApplicationController.Instance.Masks.Screen))
        {
            return;
        }
        ObjectPool.ReturnToPool(poolable);
    }


    protected void ChangePlayerScore(float points)
    {
        EventManager.OnDestroyEnemy?.Invoke(this, points);
    }
}
