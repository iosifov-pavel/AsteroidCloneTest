using UnityEngine;

public abstract class BaseController<M> : IFlyForward, IUpdateable where M : BaseModel
{
    protected M _model;
    public M Model { get => _model; set => _model = value; }

    public void FlyForward(float deltaTime, float speedScale = 1)
    {
        _model.Base.Position += _model.Base.MovementVector * deltaTime * _model.Data.Speed * speedScale;
    }

    public virtual void Setup(M model)
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

    protected abstract void CheckExitCollision(Collider2D collision, IPoolable poolable);


    protected void ChangePlayerScore(float points)
    {
        EventManager.OnDestroyEnemy?.Invoke(this, points);
    }
}
