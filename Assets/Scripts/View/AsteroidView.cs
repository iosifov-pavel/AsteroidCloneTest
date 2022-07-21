using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidView : BaseView<AsteroidModel, AsteroidController>
{
    protected override void SetCallbacks()
    {
        base.SetCallbacks();
        _model.Base.OnPositionChange.AddListener(ChangePosition);
    }

    private void ChangePosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
}
