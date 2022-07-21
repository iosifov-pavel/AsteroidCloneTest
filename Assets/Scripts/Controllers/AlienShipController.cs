using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipController : BaseController<AlienShipModel>
{
    public override void Update(float timeStep)
    {
        _model.Base.MovementVector = Utils.CalculateDirectionToPlayer(_model.Base.Position);
        _model.Base.Position += _model.Base.MovementVector * timeStep * _model.Data.Speed;
    }
}
