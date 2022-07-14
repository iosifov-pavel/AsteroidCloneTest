using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : BaseView<BulletModel, BulletController>
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

    private void Update()
    {
        if(!ReadyToUse)
        {
            return;
        }
        _controller.FlyForward(Time.deltaTime);
    }
}
