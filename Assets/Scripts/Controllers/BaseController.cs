using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController<M> : IFlyForward, IUpdateable where M : BaseModel
{
    protected M _model;
    public M Model { get => _model; set => _model = value; }

    public void FlyForward(float deltaTime)
    {
        _model.Base.Position += _model.Base.MovementVector * deltaTime * _model.Data.Speed;
    }

    public virtual void Setup(M model)
    {
        _model = model;
    }

    public abstract void Update(float timeStep);
}
