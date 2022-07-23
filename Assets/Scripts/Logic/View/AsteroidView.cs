using UnityEngine;

public class AsteroidView : BaseView<AsteroidModel, AsteroidController>
{
    protected override void SetCallbacks()
    {
        base.SetCallbacks();
        _model.Base.OnPositionChange.AddListener(ChangePosition);
        CheckSize();
    }

    private void ChangePosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    private void CheckSize()
    {
        var asteroidSize = _model.Size == AsteroidSize.Small ? Utils.Constants.AsteroidSmallSize : Utils.Constants.AsteroidBigSize;
        _body.localScale = new Vector3(asteroidSize, asteroidSize, 1);
    }
}
