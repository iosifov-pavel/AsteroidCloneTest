using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipView : BaseView<AlienShipModel,AlienShipController>
{
    protected override void SetCallbacks()
    {
        base.SetCallbacks();
        _model.Base.OnPositionChange.AddListener(ChangePosition);
        _model.Base.OnMovementChange.AddListener(ChangeRotation);
    }

    private void ChangePosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    private void ChangeRotation(Vector2 rotation)
    {
        transform.up = rotation;
    }
}
