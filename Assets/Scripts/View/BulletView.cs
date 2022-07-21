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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _controller.ProceedCollision(collision, this, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _controller.ProceedCollision(collision, this, false);
    }
}
