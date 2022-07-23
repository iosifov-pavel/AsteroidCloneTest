using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : BaseView<PlayerModel,PlayerController>
{
    [SerializeField]
    private Transform _bulletInitialPosition;
    [SerializeField]
    private AnimationCurve _laserWidth;
    [SerializeField]
    private AnimationCurve _laserLenth;
    [SerializeField]
    private Transform _laser;

    public override void Setup(PlayerModel model)
    {
        base.Setup(model);
        _controller.SetInput(_bulletInitialPosition);
        _laser.gameObject.SetActive(false);
    }
    protected override void SetCallbacks()
    {
        EventManager.OnRotationChanged += RotatePlayer;
        EventManager.OnPlayerDeath += ResetCallbacks;
        _model.Base.OnPositionChange.AddListener(MovePlayer);
        _model.OnShootLaser.AddListener(ShootLaser);
    }

    private void RotatePlayer(object sender, Vector2 newForward)
    {
        transform.up = newForward;
    }
    private void MovePlayer(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    private void ShootLaser()
    {
        StartCoroutine(LaserRoutine());
    }

    private IEnumerator LaserRoutine()
    {
        var timer = 0f;
        _laser.gameObject.SetActive(true);
        while(timer <= Utils.Constants.LaserAnimationTime)
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
