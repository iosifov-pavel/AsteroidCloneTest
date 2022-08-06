using System;
using System.Collections;
using UnityEngine;

public class PlayerView : BaseView
{
    [SerializeField]
    private Transform _bulletInitialPosition;
    [SerializeField]
    private AnimationCurve _laserWidth;
    [SerializeField]
    private AnimationCurve _laserLenth;
    [SerializeField]
    private Transform _laser;

    private new PlayerModel _model;

    public Transform BulletPosition => _bulletInitialPosition;

    public override void Setup(BaseModel model, BaseController controller, bool rotatable)
    {
        _model = (PlayerModel)model;
        base.Setup(model, controller, rotatable);
        _laser.gameObject.SetActive(false);
    }
    protected override void SetCallbacks()
    {
        base.SetCallbacks();
        _model.OnShootLaser += ShootLaser;
        _model.OnPlayerRotationChange += RotatePlayer;
    }

    private void RotatePlayer(Vector2 newForward)
    {
        transform.up = newForward;
    }

    private void ShootLaser()
    {
        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        var timer = 0f;
        _laser.gameObject.SetActive(true);
        while (timer <= PlayerModel.LaserAnimationTime)
        {
            var newLaserWidth = _laserWidth.Evaluate(timer / PlayerModel.LaserAnimationTime);
            var newLaserLength = _laserLenth.Evaluate(timer / PlayerModel.LaserAnimationTime);
            _laser.localScale = new Vector3(newLaserWidth, newLaserLength, 1);
            timer += Time.deltaTime;
            yield return null;
        }
        _laser.gameObject.SetActive(false);
    }
}
