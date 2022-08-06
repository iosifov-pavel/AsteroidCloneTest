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
        EventManager.OnRotationChanged += RotatePlayer;
        EventManager.OnPlayerDeath += ResetCallbacks;
        _model.OnShootLaser.AddListener(ShootLaser);
    }

    private void RotatePlayer(object sender, Vector2 newForward)
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
        while (timer <= Utils.Constants.LaserAnimationTime)
        {
            var newLaserWidth = _laserWidth.Evaluate(timer / Utils.Constants.LaserAnimationTime);
            var newLaserLength = _laserLenth.Evaluate(timer / Utils.Constants.LaserAnimationTime);
            _laser.localScale = new Vector3(newLaserWidth, newLaserLength, 1);
            timer += Time.deltaTime;
            yield return null;
        }
        _laser.gameObject.SetActive(false);
    }

    private void ResetCallbacks(object sender, EventArgs e)
    {
        EventManager.OnRotationChanged -= RotatePlayer;
        EventManager.OnPlayerDeath -= ResetCallbacks;
    }
}
